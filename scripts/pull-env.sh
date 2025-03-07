#!/bin/bash

project_root=$(pwd)

pull_both_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPulling dev and prod $file_name env file...\e[0m"
  cd "$service_path" &&
    npx dotenv-vault pull -y &&
    npx dotenv-vault pull -y production

  cd $project_root
}

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

pull_both_env_file "./" global_production &&
  pull_both_env_file "./app/server/APIGateway" apigateway &&
  pull_both_env_file "./app/server/IdentityService" identity &&
  pull_both_env_file "./app/server/UploadFileService" upload &&
  pull_both_env_file "./app/server/UserService" user &&
  pull_both_env_file "./app/server/RecipeService" recipe &&
  pull_both_env_file "./app/server/NotificationService" notification &&
  pull_both_env_file "./app/server/SignalRService" signalR &&
  pull_both_env_file "./app/server/TrackingService" tracking &&
  pull_both_env_file "./app/server/SubscriptionService" "subscription" &&
  pull_both_env_file "./app/server/IngredientPredictService" "ingredient-predict" &&
  pull_both_env_file "./app/client/mobile" "mobile" &&
  pull_both_env_file "./app/client/website" "website"
