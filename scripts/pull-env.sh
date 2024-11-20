#!/bin/bash

project_root=$(pwd)

pull_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPulling $file_name env file...\e[0m"
  (cd "$service_path" && npx dotenv-vault@latest pull -y)
  cd "$project_root"
}

pull_env_file "./" global && \
pull_env_file "./app/server/APIGateway" apigateway && \
pull_env_file "./app/server/IdentityService" identity && \
pull_env_file "./app/server/UploadFileService" upload && \
pull_env_file "./app/server/UserService" user && \
pull_env_file "./app/server/NotificationService" notification && \
pull_env_file "./app/server/SignalRService" signalR 
