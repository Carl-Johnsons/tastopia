#!/bin/bash

# Set project root directory
project_root=$(pwd)

build_service() {
  local service_path=$1
  local service_name=$2
  echo -e "\e[95mRestoring Nuget Package in $service_name service ...\e[0m"
  (cd "$service_path" && dotnet restore --packages "$project_root/data/nuget" --verbosity normal)
  cd "$project_root"
}

build_service "./app/server/APIGateway/src/APIGateway" "api gateway" && \
build_service "./app/server/IdentityService/DuendeIdentityServer" "identity" && \
build_service "./app/server/UploadFileService/src/UploadFileService.API" "upload"

# Publishing Contract solution
echo -e "\e[95mPublishing Contract solution ...\e[0m"
(cd ./app/server/Contract/Contract && dotnet publish)
cd "$project_root"
