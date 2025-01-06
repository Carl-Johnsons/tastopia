#!/bin/bash

project_root=$(pwd)

pull_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPulling $file_name env file...\e[0m"
  (cd "$service_path" && npx dotenv-vault pull -y)
  cd $project_root
}

pull_production_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPulling $file_name env file...\e[0m"
  (cd "$service_path" && npx dotenv-vault pull -y production)
  cd $project_root
}

pull_env_file "./" global &&
  pull_production_env_file "./" global_production &&
  pull_env_file "./app/server/APIGateway" apigateway &&
  pull_production_env_file "./app/server/APIGateway" apigateway_production &&
  pull_env_file "./app/server/IdentityService" identity &&
  pull_production_env_file "./app/server/IdentityService" identity_production &&
  pull_env_file "./app/server/UploadFileService" upload &&
  pull_env_file "./app/server/UserService" user &&
  pull_production_env_file "./app/server/UserService" user_production &&
  pull_env_file "./app/server/RecipeService" recipe &&
  pull_production_env_file "./app/server/RecipeService" recipe_production &&
  pull_env_file "./app/server/NotificationService" notification &&
  pull_production_env_file "./app/server/NotificationService" notification_production &&
  pull_env_file "./app/server/SignalRService" signalR &&
  pull_production_env_file "./app/server/TrackingService" tracking_production &&
  pull_env_file "./app/server/TrackingService" tracking &&
  pull_production_env_file "./app/server/SubscriptionService" "subscription_production" &&
  pull_env_file "./app/server/SubscriptionService" "subscription"
