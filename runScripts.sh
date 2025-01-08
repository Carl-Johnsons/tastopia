#!/bin/bash

# Imports
source ./scripts/lib.sh

break_index=3

# Read the content of scripts.json
json=$(<scripts.json)
# Remove outer object and curly braces
scripts_content=$(echo "$json" | sed 's/"scripts": //; s/{//; s/}//')
# Remove quotes, commas, leading/trailing spaces, and empty lines
scripts=$(echo "$scripts_content" | sed 's/"//g; s/,//g; s/^[[:space:]]*//; s/[[:space:]]*$//' | grep -v '^$')
# echo "$scripts"
# Extract script keys from the scripts variable
script_keys=$(echo "$scripts" | cut -d':' -f1)

check_platform 

display() {
    echo -e "Platform: ${CYAN}$PLATFORM${NC}"

    # Display available scripts with indices
    echo "Available scripts:"
    index=0
    echo "$script_keys" | while IFS= read -r line; do
        if [ $index -eq $break_index ]; then
            printf "${RED}\t***Do not run these script unless you are DevOps***${NC}\n"
        fi
        
        if [ $index -ge $break_index ]; then
            printf "\t${RED}*${NC} "
        fi
        if [ $((index % 2)) -ne 0 ]; then
            printf "$index: ${CYAN}$line${NC}\n"
        else
            printf "$index: ${LIGHT_CYAN}$line${NC}\n"
        fi
        ((index++))

    done
}

display
# Prompt the user for input
while true; do
    read -p "Enter the index of the script to run: " idx
    selected_script=$(echo "$scripts" | awk -v idx="$idx" 'NR-1 == idx { sub(/^[^:]+: /, ""); print }')
    if [ -n "$selected_script" ]; then
        echo -e "${LIGHT_BLUE}Executing script: $selected_script${NC}"
        ($selected_script)
        read -rsp $'Press any key to clear the screen...\n' -n1 key
        clear
        display
    else
        echo -e "${RED}Invalid index. Please enter a valid index.${NC}"
    fi
done
