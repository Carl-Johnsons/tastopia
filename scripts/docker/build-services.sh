#!/bin/bash

# Exit on failure
set -eo pipefail

ENV="staging"
script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"

while getopts le:c:h OPTS; do
  case $OPTS in
    e) 
      if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
        echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
        exit 1
      fi

      ENV="$OPTARG"
      ;;
    l) lFlag=1 ;;
    c) commitHash="$OPTARG" ;;
    h) cat <<EOF

Usage: $0 [options] [services]

  [services]
        A space-separated list of services to deploy.

Options:
  -c [commit]
        Commit SHA (required)

  -e [environment]   
        Specify the environment to build, accepted values
        are "dev", "staging" or "production". If omitted, 
        the default value is "staging".

  -l    Load env file based on the current specified
        environment.

  -h    Print this help.

EOF
      exit 0
      ;;
    ?) 
      echo "Unknown flag. Usage: $0 -c <commit> [-l] [-e dev|staging|production] [services]"
      exit 1
      ;;
  esac
done

if [ -z "$commitHash" ]; then
  echo "Error: -c <commit> is required."
  echo "Usage: $0 -c <commit> [-e dev|staging|production] [-l] [services]"
  exit 1
fi

# Shift parsed options
shift $((OPTIND - 1))

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

services=("$@")

if [ ${#services[@]} -eq 0 ]; then
  echo Please specify the services to build >&2
  exit 1
fi

project="$(basename $(pwd))"
project="${project,,}" # make the name lowercase
repo="taiduc113/tastopia"

# Always build contract image first
CONTRACT_HASH=$(git log -n 1 --pretty=format:%H -- app/server/Contract) # Get latest commit full SHA that touches app/server/Contract
CONTRACT_REBUILT=0
if docker manifest inspect ${repo}-contract:${CONTRACT_HASH} > /dev/null 2>&1; then
  echo "Contract image with hash ${CONTRACT_HASH} exists → skip"
else
  echo "Contract image with hash ${CONTRACT_HASH} not found → build"
  docker compose build contract
  docker tag ${project}-contract ${repo}-contract:${CONTRACT_HASH}
  docker push ${repo}-contract:${CONTRACT_HASH}
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

# Tag and push built images if they don't already exist on Docker Hub
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

  IMAGE="${serviceRepo}:${tag}"

  if docker manifest inspect "$IMAGE" > /dev/null 2>&1; then
    echo "Image ${IMAGE} already exists on Docker Hub → skipping build and push"
    continue
  fi

  echo "Building and pushing \"${service}\" to ${IMAGE}..."
  
  if [ "$service" = "website" ] && [ "$ENV" = "dev" ]; then
    echo "Preparing website env for dev build..."
    "$script_dir/../ci/build/setup-website-env.sh"
  fi

  docker compose build ${service} --build-arg CONTRACT_IMAGE=${repo}-contract:$CONTRACT_HASH 2>&1 | tee build.log
  
  echo "Pushing ${IMAGE}..."
  docker tag ${project}-${service} ${IMAGE}
  docker push ${IMAGE}
done
