#!/bin/bash

. ./scripts/lib.sh

printf "\n\t*** ${INFO}Running post build script...${NC} ***\n\n"

MOBILE_DIR="app/client/mobile/generated"
WEBSITE_DIR="app/client/website/src/generated"

mkdir -p "$WEBSITE_DIR"

# Copy the generated file to the website directory
rm -rf "$WEBSITE_DIR"
cp -r "$MOBILE_DIR" "$WEBSITE_DIR"

printf "\n\t*** ${SUCCESS}Successfully copied generated files from mobile to website directory.${NC} ***\n\n"

