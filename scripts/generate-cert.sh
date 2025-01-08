#!/bin/bash

. ./scripts/lib.sh

project_root=$(pwd)

if [ -f .env ]; then
    # Export each line as an environment variable
    export $(grep -v '^#' .env | xargs)
else
    echo ".env file not found."
fi

CertPath="$HOME/.aspnet/https"

if [[ "$PLATFORM" != "windows" ]]; then
  printf "${INFO}Generating SSL certification on ${SUCCESS}MacOS${NC} or ${SUCCESS}Linux${NC} ...\n"
  mkdir -p $CertPath
else
  printf "${INFO}Generating SSL certification on ${SUCCESS}Window${NC} ...\n"
  mkdir -p ${CertPath////\\}
fi

dotnet dev-certs https -ep $HOME$ASPNETCORE_Kestrel__Certificates__Default__Path -p $ASPNETCORE_Kestrel__Certificates__Default__Password
dotnet dev-certs https --trust

printf "${SUCCESS}Generated SSL certificate to path: '${INFO}$CertPath${NC}'\n"