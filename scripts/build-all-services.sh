#!/bin/bash

. ./scripts/lib.sh

project_root=$(pwd)

build_service() {
  local service_path=$1
  local service_name=$2
  echo -e "${PURPLE}mBuilding $service_name service ...${NC}"

  dotnet build --packages "$project_root/data/nuget" $service_path 2>&1 |
    sed -E \
      -e "/(warning|warn|wrn)/I s/.*/$(printf "${WARNING}&${NC}")/" \
      -e "/(error|err)/I s/.*/$(printf "${DANGER}&${NC}")/"
}

# Publishing Contract solution
echo -e "\e[95mPublishing Contract solution ...\e[0m"
dotnet publish --packages "$project_root/data/nuget" -o ./Published ./app/server/Contract/Contract

build_service "./app/server/APIGateway/src/APIGateway" "api gateway" &&
  build_service "./app/server/IdentityService/src/DuendeIdentityServer" "identity" &&
  build_service "./app/server/UploadFileService/src/UploadFileService.API" "upload" &&
  build_service "./app/server/UserService/src/UserService.API" "user" &&
  build_service "./app/server/NotificationService/src/NotificationService.API" notification &&
  build_service "./app/server/SignalRService/src/SignalRHub" "signalR"
