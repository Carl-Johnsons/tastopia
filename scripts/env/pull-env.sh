#!/bin/bash

# Exit on failure
set -e 

TARGET_ENV=$1

. ./scripts/lib.sh

err_token_missing() {
  printf "\n\t${LIGHT_RED}*** Infisical token is missing ❌${NC} *** . Please fill the value in .env.local.\n\n"
  exit 1
}

if [ ! -f ".env.local" ]; then
    cat <<EOF > .env.local
# Local environment variables
# Get your infisical service token on your provided account
INFISICAL_TOKEN=''
EOF
  err_token_missing
  exit
fi

source .env.local

if [ -z "${INFISICAL_TOKEN}" ]; then
  err_token_missing
  exit
fi


project_root=$(pwd)

pull_env_file() {
  local service_path=$1
  local folder_name=$2
  local environment=$3

  local output_file
  if [ "$environment" = "dev" ]; then
    output_file=".env"
  else
    output_file=".env.$environment"

    if [ "$folder_name" == "website" ] && [ "$environment" == "prod" ]; then
      output_file=".env.production"
    fi
  fi

  local prefix_folder_path="/"
  [[ "$PLATFORM" == "windows" ]] && prefix_folder_path="//"

  if [ ! -d $service_path ]; then
    if [ "$PLATFORM" != "linux" ] && [ "$PLATFORM" != "macos" ]; then
      echo "Please create folder at path \"$service_path\" before running the script"
      exit 1
    fi

    mkdir -p $service_path
    echo "Created folder at path: $service_path"
  fi

  echo -e "\e[95mPulling $prefix_folder_path$folder_name $environment env file...\e[0m"

  export INFISICAL_DISABLE_UPDATE_CHECK=true
   infisical export --token=$INFISICAL_TOKEN --path=$prefix_folder_path$folder_name --env=$environment --log-level debug \
  | sed -E "s/^([A-Z0-9_]+)='([0-9]+)'$/\1=\2/" \
  | sed -E "s/^([A-Z0-9_]+)='(.*)'$/\1=\2/" \
  > ./$service_path/$output_file
}

pull_all_services() {
  local env=$1
  printf "\n\t${INFO}=== Begin pull for $env environment ===${NC}\n"
  pull_env_file "./" global $env &&
    pull_env_file "./app/server/APIGateway" apigateway $env &&
    pull_env_file "./app/server/IdentityService" identity $env &&
    pull_env_file "./app/server/UploadFileService" upload $env &&
    pull_env_file "./app/server/UserService" user $env &&
    pull_env_file "./app/server/RecipeService" recipe $env &&
    pull_env_file "./app/server/NotificationService" notification $env &&
    pull_env_file "./app/server/SignalRService" signalr $env &&
    pull_env_file "./app/server/TrackingService" tracking $env &&
    pull_env_file "./app/server/IngredientPredictService" "ingredient-predict" $env &&
    pull_env_file "./app/client/mobile" "mobile" $env &&
    pull_env_file "./app/client/website" "website" $env
}

case "$TARGET_ENV" in
  "dev")
    pull_all_services dev
    ;;
  "prod")
    pull_all_services prod
    ;;
  "")
    # No argument provided: Pull BOTH dev and prod
    printf "${INFO}No environment specified. Pulling both 'dev' and 'prod' secrets...${NC}"
    pull_all_services dev
    pull_all_services prod
    ;;
  *)
    # Invalid argument
    printf "${DANGER}Invalid argument: '$TARGET_ENV'. Usage: $0 [dev | prod]${NC}"
    exit 1
    ;;
esac
