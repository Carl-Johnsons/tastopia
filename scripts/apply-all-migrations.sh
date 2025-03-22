#!/bin/bash

. ./scripts/lib.sh && check_docker

project_root=$(pwd)

[[ "$PLATFORM" != "windows" ]] && sudo chmod 777 data/db -R && echo -e "${GREEN}Run chmod 777 for data/db directory successfully${NC}"

update_database() {
    local env_path=$1
    local service_path=$2
    local service_name=$3

    if [ -f $env_path ]; then
        # Export each line as an environment variable
        export $(grep -v '^#' .env | xargs)
    else
        echo ".env file not found."
    fi

    echo -e "\e[96mApplying $service_name service migrations ....\e[0m"
    cd $service_path  
    dotnet ef database update
    cd "$project_root"
}

update_database "./app/server/IdentityService/.env" "./app/server/IdentityService/src/IdentityService.Infrastructure" "Identity" &&
    update_database "./app/server/UploadFileService/.env" "./app/server/UploadFileService/src/UploadFileService.Infrastructure" "Upload" &&
    update_database "./app/server/UserService/.env" "./app/server/UserService/src/UserService.Infrastructure" "User"