#!/bin/bash

. ./scripts/lib.sh && check_docker

dotnet tool install --global dotnet-ef --version 8.0.11
if [[ "$PLATFORM" != "windows" ]]; then
    grep -qxF 'export PATH="$PATH:$HOME/.dotnet/tools"' ~/.bashrc || echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
    # Source the bashrc outside of subshell process by skipping 10 lines of code in .bashrc which is checking whether the current shell process is interactive or not
    eval "$(cat ~/.bashrc | tail -n +10)"
    printf "\n\t*** ${DEBUG}Add dotnet-ef to PATH and source ~/bashrc${NC} ***\n\n"
fi

printf "\n\t*** ${SUCCESS}INSTALL dotnet-ef SUCCESSFULLY${NC} ***\n\n"

./scripts/env/pull-env.sh
printf "\n\t*** ${SUCCESS}DONE PULLING ENV${NC} ***\n\n"

run_required_docker_services

[[ "$PLATFORM" != "windows" ]] && sudo chown $(whoami) data -R && echo -e "${SUCCESS}Run chown for data directory successfully${NC}"
printf "\n\t*** ${SUCCESS}DONE RUNNING CONTAINER${NC} ***\n\n"

./scripts/local/build-all-services.sh
printf "\n\t*** ${SUCCESS}DONE BUILDING ALL SERVICES${NC} ***\n\n"

./scripts/local/apply-all-migrations.sh
printf "\n\t*** ${SUCCESS}DONE APPLY ALL MIGRATIONS${NC} ***\n\n"

./scripts/local/setup-capture-service.sh
printf "\n\t*** ${SUCCESS}DONE SETUP CAPTURE SERVICE${NC} ***\n\n"
