#!/bin/bash

. ./scripts/lib.sh && check_docker

project_root=$(pwd)

[[ "$PLATFORM" != "windows" ]] && sudo chmod 777 data/db -R && echo -e "${GREEN}Run chmod 777 for data/db directory successfully${NC}"

POSTGRES_REQUIRED_SERVICES=("Identity" "User")
MONGODB_REQUIRED_SERVICES=("Recipe" "Notification" "Tracking")

update_database() {
    local env_path=$1
    local project=$2
    local name=$3

    if [ -f $env_path ]; then
        # Export each line as an environment variable
        export $(grep -v '^#' .env | xargs)
    else
        echo ".env file not found."
    fi

    if [[ " ${POSTGRES_REQUIRED_SERVICES[@]} " =~ " ${name} " ]]; then
        echo -e "${INFO}Running Postgresql migrations for ${name}...${NC}"
        env NUGET_PACKAGES="$project_root/data/nuget" \
            dotnet run --project "$project" -- --migrate --seed
    fi

    if [[ " ${MONGODB_REQUIRED_SERVICES[@]} " =~ " ${name} " ]]; then
        echo -e "${INFO}Running MongoDB migrations for ${name}...${NC}"
        env NUGET_PACKAGES="$project_root/data/nuget" \
            dotnet run --project "$project" -- --seed
    fi
}

update_database "./app/server/IdentityService/.env" "./app/server/IdentityService/src/DuendeIdentityServer" "Identity"
update_database "./app/server/UserService/.env" "./app/server/UserService/src/UserService.API" "User"
update_database "./app/server/RecipeService/.env" "./app/server/RecipeService/src/RecipeService.API" "Recipe"
update_database "./app/server/NotificationService/.env" "./app/server/NotificationService/src/NotificationService.API" "Notification"
update_database "./app/server/TrackingService/.env" "./app/server/TrackingService/src/TrackingService.API" "Tracking"