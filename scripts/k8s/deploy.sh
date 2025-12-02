#!/bin/bash

set -o pipefail

project_root=$(pwd)
server_root="app/server"
client_root="app/client"
env_file=".env.prod"
website_env_file=".env.production"

ENV="staging"
BASE_PATH="./k8s/base"

# Paths relative to BASE_PATH
STAGING_PATH="../overlays/staging"
KUSTOMIZE_ENV_FILE="${project_root}/.env.staging"
PRODUCTION_PATH="../overlays/production"
KUSTOMIZE_PATH="$STAGING_PATH"

while getopts e:h OPTS; do
  case $OPTS in
    e) 
      if [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
        echo 'Only "staging" or "production" is allowed as value of -e flag.'
        exit 1
      fi

      ENV="$OPTARG"

      if [ "$ENV" = "staging" ]; then
        env_file=".env.staging"
        website_env_file=".env.staging"
        KUSTOMIZE_PATH="$STAGING_PATH"
        KUSTOMIZE_ENV_FILE="${project_root}/.env.staging"
      else
        KUSTOMIZE_PATH="$PRODUCTION_PATH"
        KUSTOMIZE_ENV_FILE="${project_root}/.env.prod"
      fi
      ;;
    h) cat <<EOF

Usage: $0 [options] [services]

  [services]
        A space-separated list of services to deploy.

Options:
  -e [environment]   
        Specify the environment to deploy, accepted values
        are either "staging" or "production". If omitted, 
        the default value is "staging".

EOF
      exit 0
      ;;
    ?) 
      echo "Unknown flag. Usage: $0 [-e staging|production] [services]"
      exit 1
      ;;
  esac
done

# Shift parsed options
shift $((OPTIND - 1))

hydrate_yaml() {
  set -a
  . "$KUSTOMIZE_ENV_FILE"

  find "$KUSTOMIZE_PATH" -type f -name '*.yaml' -print0 | \
    xargs -0 -P4 -I {} bash -c '
      PARENT_DIR=$(dirname "{}")
      FILE_NAME=$(basename "{}" .yaml)
      OUTPUT_FILE="${PARENT_DIR}/${FILE_NAME}.hydrated.yaml"
      envsubst < "{}" > "$OUTPUT_FILE"
    '

  set +a
}

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
  [website]="$client_root/website/$website_env_file"
)

# Creating secret

for secret in "${!generic[@]}"; do
  file="${generic[$secret]}"
  secret="${secret}-secret"

  kubectl delete secret "$secret" 2>/dev/null
  kubectl create secret generic "$secret" --from-env-file="$file"
done

# Apply file .yaml

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
  # "ingredient-predict-api"
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

cd "$BASE_PATH"
hydrate_yaml && KUSTOMIZE_YAML=$(kubectl kustomize "$KUSTOMIZE_PATH")

for service in "${services[@]}"; do
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
    if [ -f "$KUSTOMIZE_PATH/ingresses/${service}.yaml" ]; then
      echo "$KUSTOMIZE_YAML" \
        | yq "select(.kind == \"Ingress\" and .metadata.name == \"${service}\")" \
        | kubectl apply -f -
    else
      kubectl apply -f "./ingresses/${service}.yaml"
    fi
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
