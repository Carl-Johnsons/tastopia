#!/bin/bash

. ./scripts/lib.sh

echo -e "${INFO}Checking for vulnerable Website packages...${NC}"
npm --prefix "app/client/website" audit --audit-level=high
echo -e "${INFO}Checking for vulnerable Mobile packages...${NC}"
npm --prefix "app/client/mobile" audit --audit-level=high
