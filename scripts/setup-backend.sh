#!/bin/bash

. ./scripts/lib.sh && check_docker

dotnet tool install --global dotnet-ef --version 8.0.0
if [[ "$PLATFORM" != "windows" ]]; then
    echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
    . ~/.bashrc
    printf "\n\t*** ${DEBUG}Add dotnet-ef to PATH and source ~/bashrc${NC} ***\n\n"
fi

printf "\n\t*** ${SUCCESS}INSTALL dotnet-ef SUCCESSFULLY${NC} ***\n\n"

./scripts/pull-env.sh
printf "\n\t*** ${SUCCESS}DONE PULLING ENV${NC} ***\n\n"

run_required_docker_services

[[ "$PLATFORM" != "windows" ]] && sudo chown $(whoami) data -R && echo -e "${SUCCESS}Run chown for data directory successfully${NC}"
printf "\n\t*** ${SUCCESS}DONE RUNNING CONTAINER${NC} ***\n\n"

./scripts/build-all-services.sh
printf "\n\t*** ${SUCCESS}DONE BUILDING ALL SERVICES${NC} ***\n\n"

./scripts/apply-all-migrations.sh
printf "\n\t*** ${SUCCESS}DONE APPLY ALL MIGRATIONS${NC} ***\n\n"

./scripts/config-docker-compose.sh
printf "\n\t*** ${SUCCESS}DONE GENERATING DOCKER COMPOSE OVERRIDE FILE${NC} ***\n\n"

./scripts/setup-capture-service.sh
printf "\n\t*** ${SUCCESS}DONE SETUP CAPTURE SERVICE${NC} ***\n\n"
