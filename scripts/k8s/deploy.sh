#!/bin/bash

project_root=$(pwd)
server_root="app/server"
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
)

# declare -A tls=(
#   [identity-api]="identity"
#   [user-api]="user"
#   [recipe-api]="recipe"
#   [notification-api]="notification"
#   [tracking-api]="tracking"
#   [signalr]="signalr"
#   [api-gateway]="gateway"
# )

# Creating secret

for secret in "${!generic[@]}"; do
  file="${generic[$secret]}"
  secret="${secret}-secret"

  kubectl delete secret "$secret" 2>/dev/null
  kubectl create secret generic "$secret" --from-env-file="$file"
done


# for secret in "${!tls[@]}"; do
#   name="${tls[$secret]}"
#   crt="./ssl/certs/${name}.crt"
#   key="./ssl/private-key/${name}.key"
#   secret="${secret}-tls"

#   kubectl delete secret "$secret" 2>/dev/null
#   kubectl create secret tls "$secret" --cert="$crt" --key="$key"
# done

# Apply file .yaml
cd ./k8s

services=(
  "postgres"
  "mongo"
  "redis"
  "rabbitmq"
  "consul"
  # "website"
  "api-gateway"
  "signalr"
  "tracking-api"
  "upload-api"
  "identity-api"
  "notification-api"
  "recipe-api"
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

echo "Deleting all ingresses..."
kubectl delete ingress --all

for service in "${services[@]}"; do
  # kubectl apply -f deployments -f services
  echo "Deploying $service..."
  kubectl apply -f "./deployments/${service}.yaml" -f "./services/${service}.yaml"

  if [ -f "./ingresses/${service}.yaml" ]; then
    echo "Creating ingress for $service..."
    kubectl apply -f "./ingresses/${service}.yaml"
  fi
done

cd "$project_root"
