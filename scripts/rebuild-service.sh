#!/bin/sh
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

# Define the Docker Compose file name
DOCKER_COMPOSE_FILE="docker-compose.yaml"

# Dispose dangline entity in docker
prune_docker(){
    echo ""
    echo "==================================================================="
    echo ""
    echo -e "${LIGHT_RED}Clearing ${LIGHT_BLUE}dangling image...${NC}"
    docker image prune -a --force
    echo -e "${LIGHT_RED}Clearing ${LIGHT_BLUE}dangling container...${NC}"
    docker container prune --force
}
system_prune_docker(){
    echo "==================================================================="
    echo -e "${RED}Pruning ${LIGHT_BLUE}system...${NC}"
    docker system prune -a --force
}

# Function to display services
display_services() {
    clear
    echo "Available services:"
    echo "$SERVICES" | cat -n | while read -r index line; do
        if [ $((index % 2)) -ne 0 ]; then
            echo -e "$index: ${CYAN}$line${NC}"
        else
            echo -e "$index: ${LIGHT_CYAN}$line${NC}"
        fi
    done
}
# Get list of services
SERVICES=$(docker-compose -f "$DOCKER_COMPOSE_FILE" config --services)
COUNT=$(echo "$SERVICES" | wc -l | tr -d ' ')

# Function to colorize docker-compose output
colorize_output() {
    sed_expr=(
        -e "s/(Recreate|Recreated|Starting|Started|CACHED|DONE)/${LIGHT_GREEN_OCT}&${NC_OCT}/g"
        -e "s/(Recreate|Starting|DONE|build|final)/${PURPLE_OCT}&${NC_OCT}/g"
        -e "s/Warning/${YELLOW_OCT}&${NC_OCT}/g"
        -e "s/Error/${RED_OCT}&${NC_OCT}/g"
    )
    for service in $SERVICES; do
        sed_expr+=(-e "s/($service)/${BLUE_OCT}&${NC_OCT}/g")
    done

    sed -E "${sed_expr[@]}"
}

# Check if docker-compose file exists in the current directory
if [ ! -f "$DOCKER_COMPOSE_FILE" ]; then
  echo -e "${RED}Docker Compose file not found in the current directory ${NC}"
  read -p "Press Enter to exit..."
  exit 1
fi

# Main loop
while :
do



    # Check if any services were found
    if [ "$COUNT" -eq 0 ]; then
      echo -e "${RED}No services found in $DOCKER_COMPOSE_FILE${NC}"
      read -p "Press Enter to exit..."
      exit 1
    fi

    display_services

    # Prompt user to select a service by index
    echo -e "Enter the index of the service to ${LIGHT_BLUE}rebuild${NC}, 'q' to ${LIGHT_RED}quit${NC} or 'p' to ${RED}prune data${NC}: ${NC}"
    read -p "" INPUT

    # Check if the user wants to quit
    if [ "$INPUT" = "q" ]; then
      echo "Exiting..."
      exit 0
    fi

    # Check if the user wants to purge data
    if [ "$INPUT" = "p" ]; then
      system_prune_docker
      read -p "Press Enter to continue..."
      continue
    fi


    # Check if the input is a valid number
    if ! echo "$INPUT" | grep -qE '^[0-9]+$'; then
      echo -e "${RED}Invalid input. Please enter a ${LIGHT_CYAN}number.${NC}"
      read -p "Press Enter to continue..."
      continue
    fi

    # Convert input to integer
    INDEX=$(printf "%d" "$INPUT")

    # Check if the entered index is valid
    if [ "$INDEX" -lt 1 ] || [ "$INDEX" -gt "$COUNT" ]; then
      echo -e "${RED}Invalid index selected.${NC}"
      read -p "Press Enter to continue..."
    else
      # Rebuild the selected service
      SELECTED_SERVICE=$(echo "$SERVICES" | sed -n "${INDEX}p")
      echo -e "${LIGHT_BLUE}Rebuilding service: ${LIGHT_CYAN}$SELECTED_SERVICE${NC}"
      docker-compose -f "$DOCKER_COMPOSE_FILE" up --force-recreate --no-deps -d --build "$SELECTED_SERVICE" 2>&1 | colorize_output
      echo -e "Service ${LIGHT_BLUE}$SELECTED_SERVICE${NC} has been rebuilt."

      read -p "Press Enter to continue..."
    fi
done
