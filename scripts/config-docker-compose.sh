#!/bin/bash

. ./scripts/lib.sh

if [ -f .env ]; then
    # Export each line as an environment variable
    export $(grep -v '^#' .env | xargs)
else
    echo ".env file not found."
fi

CERT_PATH="$HOME/.aspnet/https"

# Generate a temporary Docker Compose override file
cat > docker-compose.override.yaml <<EOL
version: '3.8'

services:
  api-gateway:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
  identity-api:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
  recipe-api:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
  user-api:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
  notification-api:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
  upload-api:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
  recipe-worker:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
EOL

printf "${SUCCESS}Generated docker-compose.override.yaml with certificate path: ${INFO}${CERT_PATH}${NC}\n"
