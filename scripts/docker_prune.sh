#!/bin/sh

if ! docker info > /dev/null 2>&1; then
    printf "\n\t${LIGHT_RED}*** Docker is not running ❌${NC} *** . Exiting the script.\n\n"
    exit 1
fi

echo "Pruning unused container ... "
docker container prune -f

echo "Pruning dangling image ..."
docker image prune -a -f

echo "Pruning docker build cache ..."
docker buildx prune -f