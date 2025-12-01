#!/bin/bash

# Exit on failure
set -eo pipefail

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

project="$(basename $(pwd))"
repo="taiduc113/tastopia"

echo Building...
for service in "${services[@]}"; do
  echo "Building \"${service}\"..."

  if [ $service == "website" ]; then
    current_dir=$(pwd)
    cd app/client/website
    npm ci
    npm run build
    cd $current_dir
    unset current_dir
  fi

  docker compose build ${service} 2>&1 | tee build.log
done

# Tag each built image into the same repo with different tags
for service in "${services[@]}"; do
  docker tag ${project}-${service} ${repo}:${service}
  docker push ${repo}:${service}
done
