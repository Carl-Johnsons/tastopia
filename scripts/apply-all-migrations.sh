#!/bin/bash

project_root=$(pwd)

update_database() {
    local service_path=$1
    local service_name=$2

    echo -e "\e[96mApplying $service_name service migrations ....\e[0m"
    (cd $service_path && dotnet ef database update)
    cd "$project_root"
}

update_database "./app/server/IdentityService/DuendeIdentityServer" "Identity"
update_database "./app/server/UploadFileService/src/UploadFileService.Infrastructure" "Upload"
update_database "./app/server/UserService/src/UserService.Infrastructure" "User"