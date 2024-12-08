#!/bin/sh

# Define color
LIGHT_RED='\033[1;31m'
GREEN='\033[0;32m'
GREEN_OCT='\o033[0;32m'
NC='\033[0m'      # No Color

if ! docker info > /dev/null 2>&1; then
    printf "\n\t${LIGHT_RED}*** Docker is not running ❌${NC} *** . Exiting the script.\n\n"
    exit 1
fi

dotnet tool install --global dotnet-ef --version 9.0.0

./scripts/pull-env.sh
printf "\n\t*** ${GREEN}DONE PULLING ENV${NC} ***\n\n"

docker compose up -d postgres rabbitmq redis
printf "\n\t*** ${GREEN}DONE RUNNING CONTAINER${NC} ***\n\n"

./scripts/build-all-services.sh
printf "\n\t*** ${GREEN}DONE BUILDING ALL SERVICES${NC} ***\n\n"

./scripts/apply-all-migrations.sh
printf "\n\t*** ${GREEN}DONE APPLY ALL MIGRATIONS${NC} ***\n\n"