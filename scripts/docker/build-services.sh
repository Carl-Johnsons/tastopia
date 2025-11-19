#!/bin/bash

# Exit on failure
set -e

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
  docker compose build ${service}
done

# Tag each built image into the same repo with different tags
for service in "${services[@]}"; do
  docker tag ${project}-${service} ${repo}:${service}
  docker push ${repo}:${service}
done
