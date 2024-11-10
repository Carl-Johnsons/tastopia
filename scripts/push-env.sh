#!/bin/bash

# Set project root directory
project_root=$(pwd)

echo -e "\e[95mPushing global env file ....\e[0m"
npx dotenv-vault@latest push

echo -e "\e[95mPushing api gateway env file ....\e[0m"
(cd ./app/server/APIGateway && npx dotenv-vault@latest push)
cd "$project_root"

echo -e "\e[95mPushing identity file service env file ....\e[0m"
(cd ./app/server/IdentityService/DuendeIdentityServer && npx dotenv-vault@latest push)
cd "$project_root"

echo -e "\e[95mPushing upload service env file ....\e[0m"
(cd ./app/server/UploadFileService && npx dotenv-vault@latest push)
cd "$project_root"
