#!/bin/bash

# Exit on failure
set -e 

. ./scripts/lib.sh

TARGET_ENV=$1

generate_pub_env() {
  local service_path=$1
  local folder_name=$2
  local environment=$3

  local output_file
  if [ "$environment" = "dev" ]; then
    local source_file="$service_path/.env"
    output_file=".env.pub"
  else
    local source_file="$service_path/.env.$environment"
    output_file=".env.$environment.pub"

    if [ "$folder_name" == "website" ] && [ "$environment" == "prod" ]; then
      local source_file="$service_path/.env.production"
      output_file=".env.production.pub"
    fi
  fi

  if [ ! -d $service_path ]; then
    if [ "$PLATFORM" != "linux" ] && [ "$PLATFORM" != "macos" ]; then
      echo "Please create folder at path \"$service_path\" before running the script"
      exit 1
    fi

    mkdir -p $service_path
    echo "Created folder at path: $service_path"
  fi

  echo -e "\e[95mGenerating $prefix_folder_path$folder_name $environment public env file...\e[0m"

  local PUBLIC_PREFIX='NEXT_PUBLIC'
  local GENERATED_FILE=$(awk "/^$PUBLIC_PREFIX/" "$source_file")

  if [ -n "$GENERATED_FILE" ]; then
    echo "$GENERATED_FILE" > "./$service_path/$output_file" 
  fi

  unset GENERATED_FILE
}

generate() {
  local env=$1
  local services=(
    "./app/client/website website"
  )

  printf "\n\t${INFO}=== Begin generate for $env environment ===${NC}\n"
  printf "%s\n" "${services[@]}" | \
    xargs -P0 -I {} bash -c 'generate_pub_env $@' _ {} $env
}

export -f generate_pub_env
export PLATFORM

case "$TARGET_ENV" in
  "dev")
    generate dev
    ;;
  "staging")
    generate staging
    ;;
  "production")
    generate prod
    ;;
  *)
    # Invalid argument
    printf "${DANGER}Invalid argument: '$TARGET_ENV'. Usage: $0 [dev|staging|production]${NC}\n"
    exit 1
    ;;
esac
