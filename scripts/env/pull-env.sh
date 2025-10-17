#!/bin/bash
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
  fi

  local prefix_folder_path="/"
  [[ "$PLATFORM" == "windows" ]] && prefix_folder_path="//"

  echo -e "\e[95mPulling $prefix_folder_path$folder_name $environment env file...\e[0m"
  infisical export --token=$INFISICAL_TOKEN --path=$prefix_folder_path$folder_name --env=$environment --log-level debug \
  | sed -E "s/^([A-Z0-9_]+)='([0-9]+)'$/\1=\2/" \
  | sed -E "s/^([A-Z0-9_]+)='(.*)'$/\1=\2/" \
  > ./$service_path/$output_file
}

pull_both_env_file() {
  pull_env_file $1 $2 dev && pull_env_file $1 $2 prod
}

pull_both_env_file "./" global &&
  pull_both_env_file "./app/server/APIGateway" apigateway &&
  pull_both_env_file "./app/server/IdentityService" identity &&
  pull_both_env_file "./app/server/UploadFileService" upload &&
  pull_both_env_file "./app/server/UserService" user &&
  pull_both_env_file "./app/server/RecipeService" recipe &&
  pull_both_env_file "./app/server/NotificationService" notification &&
  pull_both_env_file "./app/server/SignalRService" signalr &&
  pull_both_env_file "./app/server/TrackingService" tracking &&
  pull_both_env_file "./app/server/IngredientPredictService" "ingredient-predict" &&
  pull_both_env_file "./app/client/mobile" "mobile" &&
  pull_both_env_file "./app/client/website" "website"
