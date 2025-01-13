#!/bin/bash

. ./scripts/lib.sh && check_docker

dotnet tool install --global dotnet-ef --version 9.0.0
echo "Install dotnet-ef successfully"

./scripts/pull-env.sh
printf "\n\t*** ${GREEN}DONE PULLING ENV${NC} ***\n\n"

run_required_docker_services

[[ "$PLATFORM" != "windows" ]] && sudo chown $(whoami) data -R && echo -e "${GREEN}Run chown for data directory successfully${NC}"
printf "\n\t*** ${GREEN}DONE RUNNING CONTAINER${NC} ***\n\n"

./scripts/build-all-services.sh
printf "\n\t*** ${GREEN}DONE BUILDING ALL SERVICES${NC} ***\n\n"

./scripts/apply-all-migrations.sh
printf "\n\t*** ${GREEN}DONE APPLY ALL MIGRATIONS${NC} ***\n\n"

./scripts/config-docker-compose.sh
printf "\n\t*** ${GREEN}DONE GENERATING DOCKER COMPOSE OVERRIDE FILE${NC} ***\n\n"