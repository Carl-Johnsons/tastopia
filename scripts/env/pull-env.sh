#!/bin/bash

# Exit on failure
set -e 

. ./scripts/lib.sh
# Prevent git bash auto add root prefix (e.g /app/global => C:/Program File/app/global)
MSYS_NO_PATHCONV=1
export MSYS_NO_PATHCONV

while getopts psh OPTS; do
  case $OPTS in
    p) 
      pFlag=1
      sFlag=1
      ;;
    s) 
      sFlag=1
      ;;
    h) cat <<EOF

Usage: $0 [options] [dev|staging|production]

Options:
  -p    Fetch all environments at once. This behavior only
        works when there is no environment provided, which
        by default results in a fetch for dev, staging and
        production environment. This option implies -s.

  -s    Turn on silent mode. Only warnings and errors are 
        printed.

EOF
      exit 0
      ;;
    ?) 
      echo "Unknown flag. Usage: $0 [-psh] [dev|staging|production]"
      exit 1
      ;;
  esac
done

# Shift parsed options
shift $((OPTIND - 1))

TARGET_ENV=$1

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

command -v infisical >/dev/null 2>&1 || {
  echo "infisical not installed"
  exit 1
}

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

  local prefix_folder_path="/apps/"

  if [ ! -d $service_path ]; then
    if [ "$PLATFORM" != "linux" ] && [ "$PLATFORM" != "macos" ]; then
      echo "Please create folder at path \"$service_path\" before running the script"
      exit 1
    fi

    mkdir -p $service_path
    [ -z "$sFlag" ] && echo "Created folder at path: $service_path"
  fi

  [ -z "$sFlag" ] && echo -e "\e[95mPulling $prefix_folder_path$folder_name $environment env file...\e[0m"

  PULLED_FILE=$(
    INFISICAL_DISABLE_UPDATE_CHECK=true \
    infisical export \
      --token=$INFISICAL_TOKEN \
      --path=$prefix_folder_path$folder_name \
      --env=$environment \
    | sed -E "s/^([A-Z0-9_]+)='([0-9]+)'$/\1=\2/" \
    | sed -E "s/^([A-Z0-9_]+)='(.*)'$/\1=\2/"
  )

  if [ -n "$PULLED_FILE" ]; then
    echo "$PULLED_FILE" > "$service_path/$output_file" 
  fi

  unset PULLED_FILE
}

pull_all_services() {
  local env=$1
  local services=(
    ". global"
    "./app/server/APIGateway apigateway"
    "./app/server/IdentityService identity"
    "./app/server/UploadFileService upload"
    "./app/server/UserService user"
    "./app/server/RecipeService recipe"
    "./app/server/NotificationService notification"
    "./app/server/SignalRService signalr"
    "./app/server/TrackingService tracking"
    "./app/server/IngredientPredictService ingredient-predict"
    "./app/client/mobile mobile"
    "./app/client/website website"
  )

  [ -z "$sFlag" ] && printf "\n\t${INFO}=== Begin pull for $env environment ===${NC}\n"

  printf "%s\n" "${services[@]}" | \
    xargs -P0 -I {} bash -c 'pull_env_file $@' _ {} $env
}

generate_public_env() {
  ./scripts/env/generate-public-env.sh $1
}

export -f pull_env_file
export INFISICAL_TOKEN sFlag PLATFORM

case "$TARGET_ENV" in
  "dev"|"staging"|"production")
    pull_all_services "$TARGET_ENV"
    generate_public_env "$TARGET_ENV"
    ;;
  "")
    # No argument provided: Pull dev, staging and production
    printf "${INFO}No environment specified. Pulling 'dev', 'staging' and 'production' secrets...${NC}\n"
    export -f pull_all_services generate_public_env
    
    if [ -n "$pFlag" ]; then
      echo "dev staging production" | xargs -P0 -d ' ' -I {} bash -c 'pull_all_services {}'
      echo "dev staging production" | xargs -P0 -d ' ' -I {} bash -c 'generate_public_env {}'
    else 
      echo "dev staging production" | xargs -d ' ' -I {} bash -c 'pull_all_services {}'
      echo "dev staging production" | xargs -d ' ' -I {} bash -c 'generate_public_env {}'
    fi
    ;;
  *)
    # Invalid argument
    printf "${DANGER}Invalid argument: '$TARGET_ENV'. Usage: $0 [-psh] [dev|staging|production]${NC}\n"
    exit 1
    ;;
esac
