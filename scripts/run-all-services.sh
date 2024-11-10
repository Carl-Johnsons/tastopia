#!/bin/sh

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
./kill-port.sh 127.0.0.1:5000
./kill-port.sh 127.0.0.1:5001
./kill-port.sh 127.0.0.1:5002
./kill-port.sh 127.0.0.1:5003
./kill-port.sh 127.0.0.1:5004
./kill-port.sh 127.0.0.1:5005
./kill-port.sh 127.0.0.1:5006

cd "$project_root"

# Source the .env file to load environment variables
source .env

docker compose up -d postgres rabbitmq

run_service() {
    local port=$1
    local project=$2
    local color=$3
    local name=$4

    env SA_PASSWORD="$SA_PASSWORD" \
        ASPNETCORE_ENVIRONMENT="$ASPNETCORE_ENVIRONMENT" \
        ASPNETCORE_URLS="http://0.0.0.0:$port" \
        dotnet watch run \
        --no-launch-profile \
        --project "$project" \
        2>&1 | sed "s/^/$(printf "${color}[${name}]${NC} ")/" &
}

# Run each service
run_service 5000 "./app/server/APIGateway/src/APIGateway" "$LIGHT_PURPLE" "ApiGateway"
run_service 5001 "./app/server/IdentityService/DuendeIdentityServer" "$PURPLE" "Identity"
run_service 5002 "./app/server/UploadFileService/src/UploadFileService.API" "$YELLOW" "Upload"

wait
