#!/bin/bash

. ./scripts/lib.sh && check_docker

# Common color
DANGER='\033[0;31m'
WARNING='\033[1;33m'
INFO='\033[0;32m'
DEBUG='\033[1;36m'
FATAL='\033[1;35m'
SUCCESS='\033[1;32m'

# Define color
RED='\033[0;31m'
RED_OCT='\o033[0;31m'
LIGHT_RED='\033[1;31m'
YELLOW='\033[1;33m'
YELLOW_OCT='\o033[1;33m'
LIGHT_YELLOW='\033[0;33m'
LIGHT_YELLOW_OCT='\o033[0;33m'
GREEN='\033[0;32m'
GREEN_OCT='\o033[0;32m'
LIGHT_GREEN='\033[1;32m'
LIGHT_GREEN_OCT='\o033[1;32m'
BLUE='\033[0;34m'
BLUE_OCT='\o033[0;34m'
LIGHT_BLUE='\033[1;34m'
PURPLE='\033[0;35m'
PURPLE_OCT='\o033[0;35m'
LIGHT_PURPLE='\033[1;35m'
LIGHT_PURPLE_OCT='\o033[1;35m'
CYAN='\033[0;36m'
CYAN_OCT='\o033[0;36m'
LIGHT_CYAN='\033[1;36m'
LIGHT_CYAN_OCT='\o033[1;36m'
NC='\033[0m'      # No Color
NC_OCT='\o033[0m' # No Color

project_root=$(pwd)
cd ./scripts

# kill all ports
./kill-port.sh 5000
./kill-port.sh 5001
./kill-port.sh 5002
./kill-port.sh 5003
./kill-port.sh 5004
./kill-port.sh 5005
./kill-port.sh 5006
./kill-port.sh 6000

cd "$project_root"

# Source the .env file to load environment variables
source .env

if [[ "$PLATFORM" != "windows" ]]; then
        sudo chmod 777 app/server/IdentityService/src/DuendeIdentityServer -R &&
                echo -e "${GREEN}Run chmod 777 for DuendeIdentityServer directory successfully${NC}"
fi

docker compose up -d postgres rabbitmq mongo

run_service() {
        local scheme=$1
        local port=$2
        local project=$3
        local color=$4
        local name=$5

    env NUGET_PACKAGES="$project_root/data/nuget" \
        ASPNETCORE_ENVIRONMENT="$ASPNETCORE_ENVIRONMENT" \
        ASPNETCORE_URLS="$scheme://0.0.0.0:$port" \
        dotnet watch run --non-interactive \
        --no-launch-profile \
        --project "$project" \
        2>&1 | sed -E \
        -e "/(warning|warn|wrn)/I s/.*/$(printf "${WARNING}&${NC}")/" \
        -e "/(error|err)/I s/.*/$(printf "${DANGER}&${NC}")/" \
        -e "/(information|info|inf)/I s/.*/$(printf "${INFO}&${NC}")/" \
        -e "/ is listening on/I s/.*/$(printf "${SUCCESS}&${NC}")/" \
        -e "s/^/$(printf "${color}[${name}]${NC} ")/"
}

# Run each service
run_service http 5000 "./app/server/APIGateway/src/APIGateway" "$LIGHT_PURPLE" "ApiGateway" & \ 
run_service http 5001 "./app/server/IdentityService/src/DuendeIdentityServer" "$PURPLE" "Identity" & \
run_service http 5002 "./app/server/UploadFileService/src/UploadFileService.API" "$BLUE" "Upload" & \
run_service http 5003 "./app/server/UserService/src/UserService.API" "$LIGHT_BLUE" "User" & \
run_service http 5004 "./app/server/SignalRService/src/SignalRHub" "$LIGHT_YELLOW" "SignalR" & \
run_service http 5005 "./app/server/RecipeService/src/RecipeService.API" "$LIGHT_GREEN" "Recipe" & \
run_service http 5006 "./app/server/NotificationService/src/NotificationService.API" "$LIGHT_CYAN" "Notification" & \
run_service http 6000 "./app/server/NotificationService/src/EmailWorker" "$CYAN" "Email Worker"

wait
