#!/bin/bash

# Imports
source ./scripts/lib.sh

break_index=8

if ! command -v jq >/dev/null 2>&1; then
    echo "Error: jq is required to parse scripts.json."
    exit 1
fi

# Flatten scripts into menu entries:
# Each entry contains its display name, command, and group status.
mapfile -t entries < <(
    jq -r '
        .scripts
        | to_entries[]
        | if (.value | type) == "object" then
            .key as $group
            | .value
            | to_entries[]
            | ["nested", $group, .key, .value]
            | join("|")
          else
            ["normal", "", .key, .value]
            | join("|")
          end
    ' scripts.json
)

script_types=()
script_groups=()
script_keys=()
script_commands=()

while IFS='|' read -r type group name command; do
    script_types+=("$type")
    script_groups+=("$group")
    script_keys+=("$name")
    script_commands+=("$command")
done < <(printf '%s\n' "${entries[@]}")

check_platform 

# 2 spaces instead of 4 spaces
HALF_TAB_INDENT="  "

display() {
    echo -e "Platform: ${CYAN}$PLATFORM${NC}"
    echo "Available scripts:"

    local previous_group=""
    local index
    local indent=""

    for index in "${!script_keys[@]}"; do
        if [[ "$index" -ge "$break_index" ]]; then
            indent="\t${RED}*${NC}"
        else
            indent=""
        fi

        if [[ "${script_types[$index]}" == "nested" ]]; then
            if [[ "${script_groups[$index]}" != "$previous_group" ]]; then
                printf "%b━━━ %s ━━━\n" "$indent" "${script_groups[$index]}"
                previous_group="${script_groups[$index]}"
            fi

        if [[ "$index" -ge "$break_index" ]]; then
            if [[ "$((index % 2))" -ne 0 ]]; then
                printf "%b${HALF_TAB_INDENT} » %s: ${CYAN}%s${NC}\n" "$indent" "$index" "${script_keys[$index]}"
            else
                printf "%b${HALF_TAB_INDENT} » %s: ${LIGHT_CYAN}%s${NC}\n" "$indent" "$index" "${script_keys[$index]}"
            fi
        else
            if [[ "$((index % 2))" -ne 0 ]]; then
                printf "%b${HALF_TAB_INDENT}» %s: ${CYAN}%s${NC}\n" "$indent" "$index" "${script_keys[$index]}"
            else
                printf "%b${HALF_TAB_INDENT}» %s: ${LIGHT_CYAN}%s${NC}\n" "$indent" "$index" "${script_keys[$index]}"
            fi
        fi

            continue
        fi

        previous_group=""

        if [[ "$index" -eq "$break_index" ]]; then
            printf "${RED}\t***Do not run these scripts unless you are DevOps***${NC}\n"
        fi

        if [[ "$index" -ge "$break_index" ]]; then
            printf "\t${RED}*${NC} "
        fi

        if [[ "$((index % 2))" -ne 0 ]]; then
            printf "%s: ${CYAN}%s${NC}\n" "$index" "${script_keys[$index]}"
        else
            printf "%s: ${LIGHT_CYAN}%s${NC}\n" "$index" "${script_keys[$index]}"
        fi
    done
}
for i in "${!script_keys[@]}"; do
    printf 'DEBUG index=%s key=[%s] command=[%s]\n' \
        "$i" "${script_keys[$i]}" "${script_commands[$i]}"
done


display


while true; do
    read -rp "Enter the index of the script to run: " idx

    if [[ ! "$idx" =~ ^[0-9]+$ ]] ||
       (( idx >= ${#script_commands[@]} )); then
        echo -e "${RED}Invalid index. Please enter a valid index.${NC}"
        continue
    fi

    selected_script="${script_commands[$idx]}"

    echo -e "${LIGHT_BLUE}Executing script: ${script_keys[$idx]}${NC}"

    bash -c "$selected_script"

    read -rsp $'Press any key to clear the screen...\n' -n1 key
    echo
    clear
    display
done

