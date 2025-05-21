#!/bin/bash

services=("api-gateway" "identity-api" "user-api" "recipe-api"
            "notification-api" "upload-api tracking-api" "ingredient-predict-api"
            "signalr" "email-worker" "sms-worker" "push-notification-worker" "recipe-worker" )
project="tastopia"
repo="taiduc113/tastopia"

docker compose build api-gateway identity-api \
                     user-api recipe-api notification-api \
                     upload-api tracking-api ingredient-predict-api \
                     signalr email-worker sms-worker push-notification-worker \
                     recipe-worker

# Tag each built image into the same repo with different tags
for service in "${services[@]}"; do
  docker tag ${project}-${service} ${repo}:${service}
  docker push ${repo}:${service}
done