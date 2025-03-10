#!/usr/bin/env bash

. ./scripts/lib.sh && check_docker

$SUDO_PREFIX docker stop service-bus && \
echo -e "${GREEN}Stopped service-bus${NC}" && \
$SUDO_PREFIX rm -rf data/mq && \
echo -e "${GREEN}Deleted data/mq folder${NC}" 
$SUDO_PREFIX docker rm -f service-bus && \
echo -e "${GREEN}Deleted service-bus container${NC}" 
$SUDO_PREFIX docker compose up -d rabbitmq && \
echo -e "${GREEN}Rebuilt service-bus${NC}"
