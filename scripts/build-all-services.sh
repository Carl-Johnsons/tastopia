#!/bin/bash

# Colors
PURPLE="\e[95m"
YELLOW="\033[1;33m"
RED="\e[31m"
RESET="\e[0m"

project_root=$(pwd)

build_service() {
  local service_path=$1
  local service_name=$2
  echo -e "\e[95mRestoring Nuget Package in $service_name service ...\e[0m"
  
  output=$(cd "$service_path" && dotnet build --packages "$project_root/data/nuget" 2>&1)

  echo "$output" | while IFS= read -r line; do
    if [[ $line == *"Warning"* ]]; then
      echo -e "${YELLOW}${line}${RESET}"
    elif [[ $line == *"Error"* ]]; then
      echo -e "${RED}${line}${RESET}"
    else
      echo "$line"
    fi
  done

  cd "$project_root"
}

build_service "./app/server/APIGateway/src/APIGateway" "api gateway" &&
  build_service "./app/server/IdentityService/DuendeIdentityServer" "identity" &&
  build_service "./app/server/UploadFileService/src/UploadFileService.API" "upload" &&
  build_service "./app/server/UserService/src/UserService.API" "user"

# Publishing Contract solution
echo -e "\e[95mPublishing Contract solution ...\e[0m"
(cd ./app/server/Contract/Contract && dotnet publish)
cd "$project_root"
