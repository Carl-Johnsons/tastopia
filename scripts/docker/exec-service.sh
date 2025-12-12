#!/bin/bash

. ./scripts/lib.sh

SERVICE_OUTPUT=$(docker ps --filter "name=$1" --format "{{.ID}} {{.Names}} {{.Image}}" | head -n1)

if [ -z "$SERVICE_OUTPUT" ]; then
    echo -e "${DANDER}No container found with name: $1${NC}"
    exit 1
fi

ID=$(echo "$SERVICE_OUTPUT" | awk '{print $1}')
NAME=$(echo "$SERVICE_OUTPUT" | awk '{print $2}')
IMAGE=$(echo "$SERVICE_OUTPUT" | awk '{print $3}')

echo -e "$SUCCESS Connecting to container:$NC"
echo -e "  ${INFO}ID:${NC} $ID"
echo -e "  ${INFO}Name:${NC} $NAME"
echo -e "  ${INFO}Image:${NC} $IMAGE"

docker exec -it -u root "$ID" /bin/sh

