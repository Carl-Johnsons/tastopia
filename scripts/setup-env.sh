#!/bin/bash

# Set project root directory
project_root=$(pwd)

# Pull global env file
echo -e "\e[95mPulling global env file ....\e[0m" && \
npx dotenv-vault@latest pull && \
npx dotenv-vault@latest pull production && \
echo -e "\e[95mPulling api gateway env file ....\e[0m" && \
(cd ./solutions/APIGatewaySolution/src/APIGateway && npx dotenv-vault@latest pull) && \
cd "$project_root" && \
echo -e "\e[95mPulling identity service env file ....\e[0m" && \
(cd ./solutions/IdentitySolution/DuendeIdentityServer && npx dotenv-vault@latest pull) && \
cd "$project_root" && \
echo -e "\e[95mPulling upload file service env file ....\e[0m" && \
(cd ./solutions/PostSolution/src/PostService.API && npx dotenv-vault@latest pull) && \
cd "$project_root" && \
echo -e "\e[95mPulling post service env file ....\e[0m" && \
(cd ./solutions/UploadFileSolution/src/UploadFileService.API && npx dotenv-vault@latest pull) && \
cd "$project_root"
