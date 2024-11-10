#!/bin/bash

# Set project root directory
project_root=$(pwd)

echo -e "\e[95mPulling global env file ....\e[0m"
npx dotenv-vault@latest pull

echo -e "\e[95mPulling api gateway env file ....\e[0m"
(cd ./app/server/APIGateway && npx dotenv-vault@latest pull)
cd "$project_root"

echo -e "\e[95mPulling identity file service env file ....\e[0m"
(cd ./app/server/IdentityService/DuendeIdentityServer && npx dotenv-vault@latest pull)
cd "$project_root"

echo -e "\e[95mPulling upload service env file ....\e[0m"
(cd ./app/server/UploadFileService && npx dotenv-vault@latest pull)
cd "$project_root"
