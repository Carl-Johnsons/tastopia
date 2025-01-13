#!/bin/bash

. ./scripts/lib.sh

# Set project root directory
project_root=$(pwd)

restore_service() {
  local service_path=$1
  local service_name=$2
  echo -e "${PURPLE}Restoring Nuget Package in $service_name service ...${NC}"

  dotnet restore --packages "$project_root/data/nuget" --verbosity normal $service_path 2>&1 |
    sed -E \
      -e "/(warning|warn|wrn)/I s/.*/$(printf "${WARNING}&${NC}")/" \
      -e "/(error|err)/I s/.*/$(printf "${DANGER}&${NC}")/"
}

# Publishing Contract solution
echo -e "${PURPLE}Publishing Contract solution ...${NC}"
dotnet publish --packages "$project_root/data/nuget" -o ./app/server/Contract/Contract/Published ./app/server/Contract/Contract

# copy refresh_token and access_token connect to gmail api
cp -r ./app/server/NotificationService/src/EmailWorker/bin/*/Debug/net8.0/Auth.Store ./data

restore_service "./app/server/APIGateway/src/APIGateway" "api gateway" &&
  restore_service "./app/server/IdentityService/src/DuendeIdentityServer" "identity" &&
  restore_service "./app/server/UploadFileService/src/UploadFileService.API" "upload" &&
  restore_service "./app/server/UserService/src/UserService.API" "user" &&
  restore_service "./app/server/RecipeService/src/RecipeService.API" "recipe" &&
  restore_service "./app/server/RecipeService/src/RecipeWorker" recipe_worker &&
  restore_service "./app/server/NotificationService/src/NotificationService.API" notification &&
  restore_service "./app/server/NotificationService/src/EmailWorker" email_worker &&
  restore_service "./app/server/SignalRService/src/SignalRHub" "signalR" &&
  restore_service "./app/server/TrackingService/src/TrackingService.API" "tracking"
