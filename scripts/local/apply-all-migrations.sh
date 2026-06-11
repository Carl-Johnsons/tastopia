#!/bin/bash

. ./scripts/lib.sh

TARGET_ENV=$1
ENV_FILE=".env"
case "$TARGET_ENV" in
  "dev"|"")
    ENV_FILE=".env"
    check_docker
    ;;
  "staging" | "production")
    ENV_FILE=".env.$TARGET_ENV"
    ;;
  *)
    # Invalid argument
    printf "${DANGER}Invalid argument: '$TARGET_ENV'. Usage: $0 [-psh] [dev|staging|production]${NC}\n"
    exit 1
    ;;
esac
echo "Migrating database for environment: $ENV_FILE"

if [ ! -f "$ENV_FILE" ]; then
  err_env_missing
  exit
fi

export $(grep -v '^#' $ENV_FILE | xargs)

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
        export $(grep -v '^#' $env_path | xargs)
    else
        echo "$env_path file not found."
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

update_database "./app/server/IdentityService/$ENV_FILE" "./app/server/IdentityService/src/DuendeIdentityServer" "Identity"
update_database "./app/server/UserService/$ENV_FILE" "./app/server/UserService/src/UserService.API" "User"
update_database "./app/server/RecipeService/$ENV_FILE" "./app/server/RecipeService/src/RecipeService.API" "Recipe"
update_database "./app/server/NotificationService/$ENV_FILE" "./app/server/NotificationService/src/NotificationService.API" "Notification"
update_database "./app/server/TrackingService/$ENV_FILE" "./app/server/TrackingService/src/TrackingService.API" "Tracking"