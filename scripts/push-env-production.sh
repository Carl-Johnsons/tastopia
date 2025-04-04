#!/bin/bash

project_root=$(pwd)

push_both_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPushing dev and prod $file_name env file...\e[0m"
  cd "$service_path" &&
    npx dotenv-vault push -y &&
    npx dotenv-vault push -y production

  cd $project_root
}

push_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPushing $file_name env file...\e[0m"
  (cd "$service_path" && npx dotenv-vault push -y)
  cd $project_root
}

push_production_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPushing $file_name env file...\e[0m"
  (cd "$service_path" && npx dotenv-vault push -y production)
  cd $project_root
}

push_production_env_file "./" global_production &&
  push_production_env_file "./app/server/APIGateway" apigateway &&
  push_production_env_file "./app/server/IdentityService" identity &&
  push_production_env_file "./app/server/UploadFileService" upload &&
  push_production_env_file "./app/server/UserService" user &&
  push_production_env_file "./app/server/RecipeService" recipe &&
  push_production_env_file "./app/server/NotificationService" notification &&
  push_production_env_file "./app/server/SignalRService" signalR &&
  push_production_env_file "./app/server/TrackingService" tracking &&
  push_production_env_file "./app/server/IngredientPredictService" "ingredient-predict" &&
  push_production_env_file "./app/client/mobile" "mobile" &&
  push_production_env_file "./app/client/website" "website"
