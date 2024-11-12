#!/bin/bash

project_root=$(pwd)

push_env_file() {
  local service_path=$1
  local file_name=$2
  echo -e "\e[95mPushing $file_name env file...\e[0m"
  (cd "$service_path" && npx dotenv-vault@latest push -y)
  cd "$project_root"
}

push_env_file "./" global && \
push_env_file "./app/server/APIGateway" apigateway && \
push_env_file "./app/server/IdentityService/DuendeIdentityServer" identity && \
push_env_file "./app/server/UploadFileService" upload
push_env_file "./app/server/UserService" user