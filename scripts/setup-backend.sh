#!/bin/sh

# Define color
GREEN='\033[0;32m'
GREEN_OCT='\o033[0;32m'
NC='\033[0m'      # No Color
NC_OCT='\o033[0m' # No Color

./scripts/pull-env.sh
printf "\n\t*** ${GREEN}DONE PULLING ENV${NC} ***\n\n"

docker compose up -d postgres rabbitmq redis
printf "\n\t*** ${GREEN}DONE RUNNING CONTAINER${NC} ***\n\n"

./scripts/build-all-services.sh
printf "\n\t*** ${GREEN}DONE BUILDING ALL SERVICES${NC} ***\n\n"

./scripts/apply-all-migrations.sh
printf "\n\t*** ${GREEN}DONE APPLY ALL MIGRATIONS${NC} ***\n\n"