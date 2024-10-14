#!/bin/bash

# Set project root directory
project_root=$(pwd)

# Source the .env file to load environment variables
source .env

# Define SERVER variable
SERVER="$HOST_NAME, $DB_PORT"

# Print variables
echo -e "\e[96mHostname\e[0m      '$HOST_NAME'"
echo -e "\e[96mDatabase port\e[0m '$DB_PORT'"
echo -e "\e[96mPassword\e[0m      '$SA_PASSWORD'"
echo -e "\e[96mServer\e[0m        '$SERVER'"

# Apply Upload file Service Migrations
DB="$FileDB"
echo -e "\e[96mApplying Upload file Service Migrations ....\e[0m"
echo -e "\e[96mDB=\e[0m'$DB'"
(cd ./solutions/UploadFileSolution/src/UploadFileService.Infrastructure && env SERVER="$SERVER" DB="$DB" SA_PASSWORD="$SA_PASSWORD" dotnet ef database update)
cd "$project_root"

# Apply Post Service Migrations
DB="$PostDB"
echo -e "\e[96mApplying Post Service Migrations ....\e[0m"
echo -e "\e[96mDB=\e[0m'$DB'"
(cd ./solutions/PostSolution/src/PostService.Infrastructure && env SERVER="$SERVER" DB="$DB" SA_PASSWORD="$SA_PASSWORD" dotnet ef database update)
cd "$project_root"
