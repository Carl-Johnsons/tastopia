#!/bin/sh

echo "Pruning unused container ... "
docker container prune -f

echo "Pruning dangling image ..."
docker image prune -a -f

echo "Pruning docker build cache ..."
docker buildx prune -f