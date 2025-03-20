#!/bin/bash

. ./scripts/lib.sh

project_root=$(pwd)

drop_database() {
    local env_path=$1
    local service_path=$2
    local service_name=$3

    if [ -f $env_path ]; then
        # Export each line as an environment variable
        export $(grep -v '^#' .env | xargs)
    else
        echo ".env file not found."
    fi

    echo -e "\e[96mDropping $service_name database ....\e[0m"
    cd $service_path  
    dotnet ef database drop -f
    cd "$project_root"
}

drop_database "./app/server/IdentityService/.env" "./app/server/IdentityService/src/IdentityService.Infrastructure" "Identity" &&
    drop_database "./app/server/UploadFileService/.env" "./app/server/UploadFileService/src/UploadFileService.Infrastructure" "Upload" &&
    drop_database "./app/server/UserService/.env" "./app/server/UserService/src/UserService.Infrastructure" "User" && 
    drop_database "./app/server/SubscriptionService/.env" "./app/server/SubscriptionService/src/SubscriptionService.Infrastructure" "Subscription"

