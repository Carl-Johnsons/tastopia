#!/bin/bash

services=("ingredient-predict-api")
project="tastopia"
repo="taiduc113/tastopia"

docker compose build ingredient-predict-api

# Tag each built image into the same repo with different tags
for service in "${services[@]}"; do
  docker tag ${project}-${service} ${repo}:${service}
  docker push ${repo}:${service}
done