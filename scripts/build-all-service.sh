#!/bin/bash

# Set project root directory
project_root=$(pwd)

echo -e "\e[95mRestoring Nuget Package in api gateway service ....\e[0m"
(cd ./solutions/APIGatewaySolution/src/APIGateway && dotnet restore --packages "$project_root/data/nuget" --verbosity normal)
cd "$project_root"

echo -e "\e[95mRestoring Nuget Package in post service ....\e[0m"
(cd ./solutions/PostSolution/src/PostService.API && dotnet restore --packages "$project_root/data/nuget" --verbosity normal)
cd "$project_root"

echo -e "\e[95mRestoring Nuget Package in upload file service ....\e[0m"
(cd ./solutions/UploadFileSolution/src/UploadFileService.API && dotnet restore --packages "$project_root/data/nuget" --verbosity normal)
cd "$project_root"

# Publishing Contract solution
echo -e "\e[95mPublishing Contract solution ...\e[0m"
(cd ./solutions/Contract && dotnet publish)
cd "$project_root"
