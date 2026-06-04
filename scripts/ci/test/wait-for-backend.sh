#!/bin/bash

set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)"
. "$script_dir/../lib/server.sh"

ENV="${ENV:-dev}"

while getopts e:h OPTS; do
  case $OPTS in
  e)
    if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
      echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
      exit 1
    fi

    ENV="$OPTARG"
    ;;
  h)
    cat <<EOF

Usage: $0 [options]

Options:
  -e [environment]   
        Specify the environment to build, accepted values
        are "dev", "staging" or "production". If omitted, 
        the default value is "staging".

  -h    Print this help.

EOF
    exit 0
    ;;
  ?)
    echo "Unknown flag. Usage: $0 [-e dev|staging|production]"
    exit 1
    ;;
  esac
done

shift $((OPTIND - 1))

check_argocd_sync() {
  : "${ARGOCD_AUTH_TOKEN:?ARGOCD_AUTH_TOKEN is required}"
  : "${ARGOCD_SERVER:?ARGOCD_SERVER is required}"
  : "${BUILT_IMAGES:?BUILT_IMAGES is required}"

  local app_name
  if [ "$ENV" = "dev" ]; then
    : "${PR_NUMBER:?PR_NUMBER is required for dev environment}"
    app_name="tastopia-pr-$PR_NUMBER"
  elif [ "$ENV" = "staging" ] || [ "$ENV" = "production" ]; then
    app_name="tastopia-$ENV"
  else
    echo "Invalid environment: $ENV"
    exit 1
  fi

  echo "Checking ArgoCD sync state for $app_name..."

  local timeout=300
  local interval=10
  local elapsed=0

  local -a images=()
  local -a service_names=()
  local image_state service tag

  for image_state in $BUILT_IMAGES; do
    service="${image_state%%:*}"
    tag="${image_state#*:}"

    if [ -z "$service" ] || [ -z "$tag" ] || [ "$service" = "$tag" ]; then
      echo "Invalid image state: $image_state"
      return 1
    fi

    if [ "$service" = "mobile" ]; then
      continue
    fi

    if [ "$service" = "website" ]; then
      tag="${ENV}-${tag}"
    fi

    service_names+=("$service")
    images+=("ghcr.io/carl-johnsons/tastopia-${service}:${tag}")
  done

  while true; do
    local app_yaml

    if ! app_yaml=$(argocd app get "$app_name" --grpc-web -o yaml); then
      echo "Failed to get ArgoCD app info for $app_name"
    else
      local all_deployed=true
      local deployed_images
      deployed_images=$(echo "$app_yaml" | yq '.status.summary.images[]')
      local -a deployed_services=()
      local i

      for i in "${!images[@]}"; do
        local image="${images[$i]}"
        local service_name="${service_names[$i]}"

        if ! echo "$deployed_images" | grep -Fq "ghcr.io/carl-johnsons/tastopia-${service_name}:"; then
          echo "Service $service_name not found in $app_name, skipping..."
          continue
        fi

        if ! echo "$deployed_images" | grep -Fqx "$image"; then
          echo "Image $image not yet deployed in $app_name"
          all_deployed=false
          break
        fi

        deployed_services+=("$service_name")
      done

      local all_healthy=true

      if [ "$all_deployed" = true ]; then
        for svc in "${deployed_services[@]}"; do
          local resource
          resource=$(echo "$app_yaml" | yq ".status.resources[] | select(.name == \"$svc\" and .kind == \"Deployment\")")

          if [ -z "$resource" ]; then
            continue
          fi

          local res_health res_sync
          res_health=$(echo "$resource" | yq '.health.status')
          res_sync=$(echo "$resource" | yq '.status')

          if [ "$res_health" != "Healthy" ] || [ "$res_sync" != "Synced" ]; then
            echo "Deployment $svc: sync=$res_sync health=$res_health"
            all_healthy=false
          fi
        done
      fi

      if [ "$all_deployed" = true ] && [ "$all_healthy" = true ]; then
        echo "All deployments in $app_name are Synced + Healthy"
        return 0
      fi
    fi

    if [ "$elapsed" -ge "$timeout" ]; then
      echo "Timed out waiting for ArgoCD sync for $app_name"
      return 1
    fi

    echo "Waiting for images to be deployed... ($((timeout - elapsed))s remaining)"
    sleep "$interval"
    elapsed=$((elapsed + interval))
  done
}

wait_for_server() {
  local endpoint

  if ! endpoint="$(get_api_endpoint)/health"; then
    echo "Failed to get API endpoint"
    exit 1
  fi

  check_argocd_sync

  local timeout=300
  local interval=1
  local count=0

  until curl -fsL --connect-timeout 2 --max-time 3 "$endpoint" | grep -q "Healthy"; do
    ((++count))

    if ((count * interval >= timeout)); then
      echo Timed out
      exit 1
    fi

    echo "Waiting for backend..."
    sleep $interval
  done

  echo "Server is online"
}

wait_for_website() {
  local endpoint

  if ! endpoint="$(get_client_base_url)"; then
    echo "Failed to get website endpoint"
    exit 1
  fi

  check_argocd_sync

  local timeout=300
  local interval=2
  local elapsed=0

  until curl -fsL --connect-timeout 2 --max-time 5 "$endpoint" >/dev/null; do
    elapsed=$((elapsed + interval))

    if [ "$elapsed" -ge "$timeout" ]; then
      echo "Timed out waiting for website"
      exit 1
    fi

    echo "Waiting for website..."
    sleep "$interval"
  done

  echo "Website is online"
}

if [[ "${BASH_SOURCE[0]}" == "${0}" ]]; then
  wait_for_server
fi
