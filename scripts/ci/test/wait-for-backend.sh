#!/bin/bash

set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
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
    h) cat <<EOF

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

load_env() {
  local script_dir=$(cd $(dirname "${BASH_SOURCE[0]}") && pwd)
  suffix="" 

  if [ "$ENV" != "dev" ]; then
    suffix=".$ENV"
  fi

  set -a
  . "$script_dir/../../../.env$suffix"
  set +a
  unset suffix
}

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

  IFS=',' read -ra images <<< "$BUILT_IMAGES"

  while true; do
    local app_yaml
    if ! app_yaml=$(argocd app get "$app_name" --grpc-web -o yaml); then
      echo "Failed to get ArgoCD app info for $app_name"
    else
      local all_deployed=true
      local deployed_images=$(echo "$app_yaml" | yq '.status.summary.images[]')

      for image in "${images[@]}"; do
        local service_name="${image%%:*}"
        
        if ! echo "$deployed_images" | grep -qE "^$service_name(:|$)"; then
          echo "Service $service_name not found in $app_name, skipping..."
          continue
        fi

        if ! echo "$deployed_images" | grep -qx "$image"; then
          echo "Image $image not yet deployed in $app_name"
          all_deployed=false
          break
        fi
      done

      local sync_status=$(echo "$app_yaml" | yq '.status.sync.status')

      if [ "$all_deployed" = true ] && [ "$sync_status" = "Synced" ]; then
        echo "All built images are deployed and app $app_name is Synced"
        return 0
      fi

      if [ "$all_deployed" = true ]; then
        echo "Images are deployed but app $app_name is still $sync_status..."
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

  local timeout=60
  local interval=1
  local count=0

  until curl -fsL --connect-timeout 2 --max-time 3 "$endpoint" | grep -q "Healthy"; do
    ((++count))
    
    if (( count * interval >= timeout )); then
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

  local timeout=120
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
  load_env
  wait_for_server
fi
