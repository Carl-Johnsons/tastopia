#!/bin/bash

services=("website")
project="tastopia"
repo="taiduc113/tastopia"

docker compose build website

# Tag each built image into the same repo with different tags
for service in "${services[@]}"; do
  docker tag ${project}-${service} ${repo}:${service}
  docker push ${repo}:${service}
done