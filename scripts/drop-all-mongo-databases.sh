#!/usr/bin/env bash

. ./scripts/lib.sh

project_root=$(pwd)
env_file_path="$project_root/.env"

load_env() {
  # Load environment variables from .env file
  if [ -f $env_file_path ]; then
    export $(grep -v '^#' $env_file_path | xargs)
  else
    echo "❌ .env file not found in project_root!"
    exit 1
  fi
}

drop_database() {
  local db_name=$1
  local MONGO_URI="mongodb://$MONGO_INITDB_ROOT_USERNAME:$MONGO_INITDB_ROOT_PASSWORD@localhost:2002/"

  # Ensure MONGO_URI is set
  if [ -z "$MONGO_URI" ]; then
    echo "❌ MONGO_URI is not set in the .env file."
    exit 1
  fi

  # Drop the database
  echo "Attempting to drop database: $db_name..."
  mongosh "$MONGO_URI" --eval "db.getSiblingDB('$db_name').dropDatabase()"

  echo "✅ Database '$db_name' has been dropped (if it existed)."
}

load_env
drop_database RecipeDB 
drop_database TrackingDB 
drop_database NotificationDB 
