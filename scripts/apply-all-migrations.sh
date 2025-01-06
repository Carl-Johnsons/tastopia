#!/bin/bash

. ./scripts/lib.sh && check_docker

project_root=$(pwd)

[[ "$PLATFORM" != "windows" ]] && sudo chmod 777 data/db -R && echo -e "${GREEN}Run chmod 777 for data/db directory successfully${NC}"

update_database() {
    local service_path=$1
    local service_name=$2

    echo -e "\e[96mApplying $service_name service migrations ....\e[0m"
    (cd $service_path && dotnet ef database update)
    cd "$project_root"
}

update_database "./app/server/IdentityService/src/IdentityService.Infrastructure" "Identity" &&
    update_database "./app/server/UploadFileService/src/UploadFileService.Infrastructure" "Upload" &&
    update_database "./app/server/UserService/src/UserService.Infrastructure" "User" && 
    update_database "./app/server/SubscriptionService/src/SubscriptionService.Infrastructure" "Subscription"
