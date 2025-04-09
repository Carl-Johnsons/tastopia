#!/bin/bash

. ./scripts/lib.sh

if [ -f .env ]; then
    # Export each line as an environment variable
    export $(grep -v '^#' .env | xargs)
else
    echo ".env file not found."
fi

CERT_PATH="./ssl/certs"
KEY_PATH="./ssl/private-key"
CONTAINER_CERT_PATH="/etc/ssl/certs/server-cert.crt"
CONTAINER_KEY_PATH="/etc/ssl/private/private-key.pem"

# Generate a temporary Docker Compose override file
cat > docker-compose.override.yml <<EOL
services:
  api-gateway:
    volumes:
      - $CERT_PATH/gateway.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/gateway.key:$CONTAINER_KEY_PATH
  identity-api:
    volumes:
      - $CERT_PATH/identity.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/identity.key:$CONTAINER_KEY_PATH
  recipe-api:
    volumes:
      - $CERT_PATH/recipe.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/recipe.key:$CONTAINER_KEY_PATH
  user-api:
    volumes:
      - $CERT_PATH/user.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/user.key:$CONTAINER_KEY_PATH
  notification-api:
    volumes:
      - $CERT_PATH/notification.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/notification.key:$CONTAINER_KEY_PATH
  upload-api:
    volumes:
      - $CERT_PATH/upload.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/upload.key:$CONTAINER_KEY_PATH
  tracking-api:
    volumes:
      - $CERT_PATH/tracking.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/tracking.key:$CONTAINER_KEY_PATH
  signalr:
    volumes:
      - $CERT_PATH/signalr.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/signalr.key:$CONTAINER_KEY_PATH
  recipe-worker:
    volumes:
      - $CERT_PATH/recipe-worker.crt:$CONTAINER_CERT_PATH
      - $KEY_PATH/recipe-worker.key:$CONTAINER_KEY_PATH
EOL

printf "${SUCCESS}Generated docker-compose.override.yml with certificate path: ${INFO}${CERT_PATH}${NC}\n"
