#!/bin/bash

# Exit on failure
set -eo pipefail

ENV="${ENV:-staging}"

while getopts let:c:h OPTS; do
  case $OPTS in
    e) 
      if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
        echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
        exit 1
      fi

      ENV="$OPTARG"
      ;;
    t) tFlag=1 ;;
    l) lFlag=1 ;;
    c) commitHash="$OPTARG" ;;
    h) cat <<EOF

Usage: $0 [options] [services]

  [services]
        A space-separated list of services to deploy.

Options:
  -c [commit]
        Commit SHA (required in legacy mode)

  -e [environment]   
        Specify the environment to build, accepted values
        are "dev", "staging" or "production". If omitted, 
        the default value is "staging".

  -t    Turn on tag reading mode. Inputs are now treated
        with the assumed format "<service>:<tag>". For
        example, "identity-api:be06a86d".

  -l    Load env file based on the current specified
        environment.

  -h    Print this help.

EOF
      exit 0
      ;;
    ?) 
      echo "Unknown flag. Usage: $0 [-c <commit>] [-lth] [-e dev|staging|production] [services]"
      exit 1
      ;;
  esac
done

# Shift parsed options
shift $((OPTIND - 1))

# Returns 0 if the image exists in the registry AND is a real container image
# (not an SLSA in-toto attestation artifact). Attestation manifests only
# contain a single layer with mediaType "application/vnd.in-toto+json",
# so any manifest that contains that string is skipped and treated as missing.
is_real_image() {
  local image="$1"
  local manifest
  manifest=$(docker manifest inspect "$image" 2>/dev/null) || return 1
  echo "$manifest" | grep -q 'in-toto' && return 1
  return 0
}


retry_push() {
  local image="$1"
  local max_attempts=3
  local delay=5

  for attempt in $(seq 1 $max_attempts); do
    echo "Pushing ${image} (attempt ${attempt}/${max_attempts})..."

    if docker push "${image}"; then
      if is_real_image "${image}"; then
        return 0
      fi
      echo "WARNING: Push succeeded but ${image} resolved to an attestation artifact, not a real image. Retrying..."
    fi

    if [ "$attempt" -lt "$max_attempts" ]; then
      echo "Push failed. Retrying in ${delay}s..."
      sleep "$delay"
      delay=$((delay * 2))
    fi
  done

  echo "ERROR: Failed to push ${image} after ${max_attempts} attempts."
  return 1
}

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
services=("$@")
project="$(basename $(pwd))"
project="${project,,}" # make the name lowercase
repo="ghcr.io/carl-johnsons/tastopia"

if [ ${#services[@]} -eq 0 ]; then
  echo Please specify the services to build >&2
  exit 1
fi

default_services=(
  "website"
  "api-gateway"
  "signalr"
  "tracking-api"
  "upload-api"
  "identity-api"
  "notification-api"
  "recipe-api"
  "user-api"
  "ingredient-predict-api"
  "email-worker"
  "push-notification-worker"
  "recipe-worker"
  "sms-worker"
)


# Build services in the standard way. In this mode, inputs are expected to be a 
# simple list. For example, "website recipe-worker identity-api".
#
# This mode is used to build images with the provided -c <commitHash> as their tags.
build_services_legacy() {
  if [ -z "$commitHash" ]; then
    echo "Error: -c <commit> is required."
    echo "Usage: $0 -c <commit> [-e dev|staging|production] [-l] [services]"
    exit 1
  fi

  dotnet_services=(
    "api-gateway"
    "signalr"
    "tracking-api"
    "upload-api"
    "identity-api"
    "notification-api"
    "recipe-api"
    "user-api"
    "email-worker"
    "push-notification-worker"
    "recipe-worker"
    "sms-worker"
  )

  # Always build contract image first
  CONTRACT_HASH=$(git log -n 1 --pretty=format:%H -- app/server/Contract) # Get latest commit full SHA that touches app/server/Contract
  CONTRACT_REBUILT=0

  if docker manifest inspect "${repo}-contract:${CONTRACT_HASH}" > /dev/null 2>&1; then
    echo "Contract image with hash ${CONTRACT_HASH} exists → skip"
  else
    echo "Contract image with hash ${CONTRACT_HASH} not found → build"
    docker compose build contract
    docker tag ${project}-contract ${repo}-contract:${CONTRACT_HASH}
    retry_push ${repo}-contract:${CONTRACT_HASH}
    CONTRACT_REBUILT=1
  fi

  if [ "$CONTRACT_REBUILT" -eq 1 ]; then
    echo "Contract was rebuilt → forcing rebuild of all dotnet services"
    # Add all dotnet services to the services array if not already present
    for dotnet_service in "${dotnet_services[@]}"; do
      if [[ ! " ${services[@]} " =~ " ${dotnet_service} " ]]; then
        services+=("$dotnet_service")
        echo "Added ${dotnet_service} to build list"
      fi
    done
  fi

  # Tag and push built images if they don't already exist in the container registry
  for service in "${services[@]}"; do
    if ! printf '%s\n' "${default_services[@]}" | grep -qxF "$service"; then
      continue
    fi

    serviceRepo=${repo}-${service}

    if [ "$service" = "website" ]; then
      tag="${ENV}-${commitHash}"
    else
      tag="${commitHash}"
    fi

    image="${serviceRepo}:${tag}"

    if is_real_image "$image"; then
      echo "Image ${image} already exists in the container registry → skipping build and push"
      continue
    fi

    echo "Building and pushing \"${service}\" to ${image}..."
    
    if [ "$service" = "website" ] && [ "$ENV" = "dev" ]; then
      echo "Preparing website env for dev build..."
      "$script_dir/../ci/build/setup-website-env.sh"
    fi

    docker compose build ${service} --build-arg CONTRACT_IMAGE=${repo}-contract:$CONTRACT_HASH 2>&1 | tee build.log
    
    echo "Pushing ${image}..."
    docker tag ${project}-${service} ${image}
    retry_push ${image}
  done
}

load_env() {
  export ENV

  if [ -n "$lFlag" ]; then
    SUFFIX="" 

    if [ "$ENV" != "dev" ]; then
      SUFFIX=".$ENV"
    fi

    set -a
    . "$script_dir/../../.env$SUFFIX"
    set +a
    echo "loaded .env$SUFFIX file"
    unset SUFFIX
  fi
}

# Build services using a list with predetermined tag for each services.
# Inputs are now treated with the assumed format "<service>:<tag>". For 
# example, "identity-api:be06a86d recipe-api:999f9f12".
#
# This mode allow services to be built with their respective tag, allowing 
# the image tags to reflect the right commit sha that modified the service.
build_services() {
  # Always build contract image first
  if grep -q 'contract' <<< "${services[*]}" 2>/dev/null; then
    image=$(printf '%s\n' "${services[@]}" | grep '^contract:')
    contract_hash="${image#*:}"
  else
    contract_hash="$(git log -n 1 --pretty=format:%H -- app/server/Contract)"
  fi

  if docker manifest inspect "${repo}-contract:${contract_hash}" > /dev/null 2>&1; then
    echo "Contract image with hash ${contract_hash} exists → skip"
  else
    echo "Contract image with hash ${contract_hash} not found → build"
    docker compose build contract
    docker tag ${project}-contract ${repo}-contract:${contract_hash}
    retry_push ${repo}-contract:${contract_hash}
  fi
  
  # Tag and push built images if they don't already exist in the container registry
  for image in "${services[@]}"; do
    service="${image%%:*}"

    if ! printf '%s\n' "${default_services[@]}" | grep -qxF "$service"; then
      continue
    fi

    commitHash="${image#*:}"
    serviceRepo=${repo}-${service}

    if [ "$service" = "website" ]; then
      tag="${ENV}-${commitHash}"
    else
      tag="${commitHash}"
    fi

    image="${serviceRepo}:${tag}"

    if is_real_image "$image"; then
      echo "Image ${image} already exists in the container registry → skipping build and push"
      continue
    fi

    echo "Building and pushing \"${service}\" to ${image}..."
    
    if [ "$service" = "website" ] && [ "$ENV" = "dev" ]; then
      echo "Preparing website env for dev build..."
      "$script_dir/../ci/build/setup-website-env.sh"
    fi

    docker compose build ${service} --build-arg CONTRACT_IMAGE=${repo}-contract:$contract_hash 2>&1 | tee build.log
    
    echo "Pushing ${image}..."
    docker tag ${project}-${service} ${image}
    retry_push ${image}
  done
}

load_env

if [ -n "${tFlag:-}" ]; then
  build_services
else
  build_services_legacy
fi
