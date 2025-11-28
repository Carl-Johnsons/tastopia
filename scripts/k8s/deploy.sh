#!/bin/bash

set -eo pipefail

project_root=$(pwd)
server_root="app/server"
client_root="app/client"
env_file=".env.prod"
cd ./k8s

# Declare secret
cd "$project_root"

declare -A generic=(
  [global]="$env_file"
  [identity-api]="$server_root/IdentityService/$env_file"
  [user-api]="$server_root/UserService/$env_file"
  [recipe-api]="$server_root/RecipeService/$env_file"
  [notification-api]="$server_root/NotificationService/$env_file"
  [upload-api]="$server_root/UploadFileService/$env_file"
  [tracking-api]="$server_root/TrackingService/$env_file"
  [signalr]="$server_root/SignalRService/$env_file"
  [api-gateway]="$server_root/APIGateway/$env_file"
  [ingredient-predict-api]="$server_root/IngredientPredictService/$env_file"
  [website]="$client_root/website/$env_file"
)

# Creating secret

for secret in "${!generic[@]}"; do
  file="${generic[$secret]}"
  secret="${secret}-secret"

  kubectl delete secret "$secret" 2>/dev/null
  kubectl create secret generic "$secret" --from-env-file="$file"
done

# Apply file .yaml
cd ./k8s

default_services=(
  "postgres"
  "mongo"
  "redis"
  "rabbitmq"
  "consul"
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

if [ ${#services[@]} -eq 0 ] || [ "$1" == "default" ] ; then
  services=("${default_services[@]}")
fi

# echo "deleting all deployments..."
# kubectl delete deployment --all

# echo "Deleting all services..."
# kubectl delete svc --all

# echo "Deleting all persistent volumes claims..."
# kubectl delete pvc --all

# echo "Deleting all ingresses..."
# kubectl delete ingress --all

for service in "${services[@]}"; do
  # kubectl apply -f deployments -f services
  echo -e "\nDeploying $service..."

  if [ -f "./deployments/${service}.yaml" ]; then
    echo "Checking changes for deployment..."

    if kubectl diff -f "./deployments/${service}.yaml" &>/dev/null \
       && kubectl get deployment "$service" &>/dev/null; then
        echo "Deployment $service currently exists, restarting due to no new configs exist..."
        kubectl rollout restart deployment "$service"
    else
      kubectl apply -f "./deployments/${service}.yaml"
    fi
  fi

  if [ -f "./services/${service}.yaml" ]; then
    kubectl apply -f "./services/${service}.yaml"
  fi

  if [ -f "./ingresses/${service}.yaml" ]; then
    kubectl apply -f "./ingresses/${service}.yaml"
  fi
done

default_configs=(
  "nginx"
)

for config in "${default_configs[@]}"; do
  echo -e "\nApplying $config config..."
  kubectl apply -f "./configMaps/${config}.yaml"
done

cd "$project_root"
