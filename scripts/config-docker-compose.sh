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
cat > docker-compose.override.yml <<EOL
services:
  api-gateway:
    volumes:
      - ./ssl/certs/gateway.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/gateway.key:/etc/ssl/private/private-key.pem
  identity-api:
    volumes:
      - ./ssl/certs/identity.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/identity.key:/etc/ssl/private/private-key.pem
  recipe-api:
    volumes:
      - ./ssl/certs/recipe.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/recipe.key:/etc/ssl/private/private-key.pem
  user-api:
    volumes:
      - ./ssl/certs/user.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/user.key:/etc/ssl/private/private-key.pem
  notification-api:
    volumes:
      - ./ssl/certs/notification.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/notification.key:/etc/ssl/private/private-key.pem
  upload-api:
    volumes:
      - ./ssl/certs/upload.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/upload.key:/etc/ssl/private/private-key.pem
  tracking-api:
    volumes:
      - ./ssl/certs/tracking.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/tracking.key:/etc/ssl/private/private-key.pem
  signalr:
    volumes:
      - ./ssl/certs/signalr.crt:/etc/ssl/certs/server-cert.crt
      - ./ssl/private-key/signalr.key:/etc/ssl/private/private-key.pem
      - ./ssl/certs/server-cert.crt:/etc/ssl/certs/public-server-cert.crt
      - ./ssl/private-key/server.key:/etc/ssl/private/private-server-key.pem
  recipe-worker:
    volumes:
      - ${CERT_PATH}:/.aspnet/https
EOL

printf "${SUCCESS}Generated docker-compose.override.yml with certificate path: ${INFO}${CERT_PATH}${NC}\n"
