#!/bin/bash

# Set project root directory
project_root=$(pwd)

# push global env file
echo -e "\e[95mPushing global env file ....\e[0m"
npx dotenv-vault@latest push
npx dotenv-vault@latest push production

# push API Gateway env file
echo -e "\e[95mPushing api gateway env file ....\e[0m"
(cd ./solutions/APIGatewaySolution/src/APIGateway && npx dotenv-vault@latest push)
cd "$project_root"

# push Upload File Service env file
echo -e "\e[95mPushing upload file service env file ....\e[0m"
(cd ./solutions/PostSolution/src/PostService.API && npx dotenv-vault@latest push)
cd "$project_root"

# push Post Service env file
echo -e "\e[95mPushing post service env file ....\e[0m"
(cd ./solutions/UploadFileSolution/src/UploadFileService.API && npx dotenv-vault@latest push)
cd "$project_root"
