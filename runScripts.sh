#!/bin/bash
# Define color
RED='\033[0;31m'
RED_OCT='\o033[0;31m'
LIGHT_RED='\033[1;31m'
YELLOW='\033[1;33m'
YELLOW_OCT='\o033[1;33m'
LIGHT_YELLOW='\033[0;33m'
LIGHT_YELLOW_OCT='\o033[0;33m'
GREEN='\033[0;32m'
GREEN_OCT='\o033[0;32m'
LIGHT_GREEN='\033[1;32m'
LIGHT_GREEN_OCT='\o033[1;32m'
BLUE='\033[0;34m'
BLUE_OCT='\o033[0;34m'
LIGHT_BLUE='\033[1;34m'
PURPLE='\033[0;35m'
PURPLE_OCT='\o033[0;35m'
LIGHT_PURPLE='\033[1;35m'
LIGHT_PURPLE_OCT='\o033[1;35m'
CYAN='\033[0;36m'
CYAN_OCT='\o033[0;36m'
LIGHT_CYAN='\033[1;36m'
LIGHT_CYAN_OCT='\o033[1;36m'
NC='\033[0m' # No Color
NC_OCT='\o033[0m' # No Color

# Read the content of scripts.json
json=$(<scripts.json)
# Remove outer object and curly braces
scripts_content=$(echo "$json" | sed 's/"scripts": //; s/{//; s/}//')
# Remove quotes, commas, leading/trailing spaces, and empty lines
scripts=$(echo "$scripts_content" | sed 's/"//g; s/,//g; s/^[[:space:]]*//; s/[[:space:]]*$//' | grep -v '^$')
echo "$scripts"
# Extract script keys from the scripts variable
script_keys=$(echo "$scripts" | cut -d':' -f1)

display(){
    # Display available scripts with indices
    echo "Available scripts:"
    index=0
    echo "$script_keys" | while IFS= read -r line; do
        if [ $((index % 2)) -ne 0 ]; then
            echo -e "$index: ${CYAN}$line${NC}"
        else
            echo -e "$index: ${LIGHT_CYAN}$line${NC}"
        fi
        ((index++))
        
    done
}

display
# Prompt the user for input
while true; do
    read -p "Enter the index of the script to run: " idx
    selected_script=$(echo "$scripts" | awk -v idx="$idx" '{ if (NR-1 == idx) print $2 }')
    echo "$selected_script"
    if [ -n "$selected_script" ]; then
        echo -e "${LIGHT_BLUE}Executing script: $selected_script${NC}"
        bash "$selected_script"
        read -rsp $'Press any key to clear the screen...\n' -n1 key  
        clear
        display
    else
        echo -e "${RED}Invalid index. Please enter a valid index.${NC}"
    fi
done
