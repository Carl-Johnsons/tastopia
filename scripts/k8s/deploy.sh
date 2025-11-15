#!/bin/bash

project_root=$(pwd)

cd ./k8s

# Declare secret
cd "$project_root"

declare -A generic=(
  [global]=".env.prod"
  [identity-api]="app/server/IdentityService/.env.prod"
  [user-api]="app/server/UserService/.env.prod"
  [recipe-api]="app/server/RecipeService/.env.prod"
  [notification-api]="app/server/NotificationService/.env.prod"
  [upload-api]="app/server/UploadFileService/.env.prod"
  [tracking-api]="app/server/TrackingService/.env.prod"
  [signalr]="app/server/SignalRService/.env.prod"
  [api-gateway]="app/server/APIGateway/.env.prod"
  [ingredient-predict-api]="app/server/IngredientPredictService/.env.prod"
)

declare -A tls=(
  [identity-api]="identity"
  [user-api]="user"
  [recipe-api]="recipe"
  [notification-api]="notification"
  [tracking-api]="tracking"
  [signalr]="signalr"
  [api-gateway]="gateway"
)

# Creating secret

for secret in "${!generic[@]}"; do
  file="${generic[$secret]}"
  secret="${secret}-secret"

  kubectl delete secret "$secret" 2>/dev/null
  kubectl create secret generic "$secret" --from-env-file="$file"
done


for secret in "${!tls[@]}"; do
  name="${tls[$secret]}"
  crt="./ssl/certs/${name}.crt"
  key="./ssl/private-key/${name}.key"
  secret="${secret}-tls"

  kubectl delete secret "$secret" 2>/dev/null
  # kubectl create secret tls "$secret" --cert="$crt" --key="$key"
done

# Apply file .yaml
cd ./k8s

services=(
  "postgres"
  # "mongo"
  "redis"
  "rabbitmq"
  "consul"
  # "website"
  "api-gateway"
  # "signalr"
  # "tracking-api"
  "upload-api"
  "identity-api"
  # "notification-api"
  # "recipe-api"
  "user-api"
  # "ingredient-predict-api"
  # "email-worker"
  # "push-notification-worker"
  # "recipe-worker"
  # "sms-worker"
)

echo "deleting all deployments..."
kubectl delete deployment --all

echo "Deleting all services..."
kubectl delete svc --all

echo "Deleting all persistent volumes claims..."
kubectl delete pvc --all

for service in "${services[@]}"; do
  # kubectl apply -f deployments -f services
  echo "Deploying $service..."
  kubectl apply -f "./deployments/${service}.yaml" -f "./services/${service}.yaml"
done

cd "$project_root"
