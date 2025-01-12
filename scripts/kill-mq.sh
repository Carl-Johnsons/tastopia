#!/usr/bin/env bash

. ./scripts/lib.sh && check_docker

$SUDO_PREFIX docker stop service-bus && \
echo -e "${GREEN}Stopped service-bus${NC}" && \
$SUDO_PREFIX rm -rf data/mq && \
echo -e "${GREEN}Deleted data/mq folder${NC}" && \
$SUDO_PREFIX docker start service-bus && \
echo -e "${GREEN}Started service-bus${NC}"
