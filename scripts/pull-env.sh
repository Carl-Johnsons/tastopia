#!/bin/bash

project_root=$(pwd)

init() {
  echo -e "\e[95mSetting up dotenv-vault's environment...\e[0m"
  npm i -g dotenv-vault
}

clean() {
  echo -e "\e[95mCleaning up...\e[0m"
  npm un -g dotenv-vault
}

pull_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPulling $file_name env file...\e[0m"
  (cd "$service_path" && dotenv-vault pull -y)
  cd $project_root
}

init
pull_env_file "./" global && \
pull_env_file "./app/server/ApiGateway" apigateway && \
pull_env_file "./app/server/IdentityService" identity && \
pull_env_file "./app/server/UploadFileService" upload && \
pull_env_file "./app/server/UserService" user && \
pull_env_file "./app/server/NotificationService" notification && \
pull_env_file "./app/server/SignalRService" signalR 
clean
