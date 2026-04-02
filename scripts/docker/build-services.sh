#!/bin/bash

# Exit on failure
set -eo pipefail

ENV="staging"

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
  SCRIPT_DIR=$(cd $(dirname "${BASH_SOURCE[0]}") && pwd)
  SUFFIX="" 

  if [ "$ENV" != "dev" ]; then
    SUFFIX=".$ENV"
  fi

  set -a
  . "$SCRIPT_DIR/../../.env$SUFFIX"
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

services=("$@")

if [ ${#services[@]} -eq 0 ]; then
  services=("${default_services[@]}")
fi

services=("${services[@]}")

project="$(basename $(pwd))"
project="${project,,}" # make the name lowercase
repo="taiduc113/tastopia"

# Always build contract image first
CONTRACT_HASH=$(git rev-parse --short HEAD:app/server/Contract)
if [[ "$commitHash" != "$CONTRACT_HASH" ]]; then
  echo "Contract unchanged → skip build"
  docker compose build contract
  docker tag ${project}-contract ${serviceRepo}:${commitHash}
  docker push ${serviceRepo}:${commitHash}
else
  echo "Contract unchanged → skip build"
  docker tag ${project}-contract ${serviceRepo}:${CONTRACT_HASH}
fi

# Build each service
echo Building...
for service in "${services[@]}"; do
  echo "Building \"${service}\"..."
  docker compose build ${service} --build-arg CONTRACT_IMAGE=contract:$CONTRACT_HASH 2>&1 | tee build.log
done

# Tag each built image into the same repo with different tags
for service in "${services[@]}"; do
  serviceRepo=${repo}-${service}
  echo "Pushing to repo ${serviceRepo} with tag ${commitHash}"
  docker tag ${project}-${service} ${serviceRepo}:${commitHash}
  docker push ${serviceRepo}:${commitHash}
done
