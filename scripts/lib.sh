#!/usr/bin/env bash

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
NC='\033[0m'      # No Color
NC_OCT='\o033[0m' # No Color

check_platform() {
  local platform="Unknown"

  if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    platform="linux"
  elif [[ "$OSTYPE" == "darwin"* ]]; then
    platform="macos"
  elif [[ "$OSTYPE" == "cygwin" ]]; then
    platform="windows"
  elif [[ "$OSTYPE" == "msys" ]]; then
    platform="windows"
  elif [[ "$OSTYPE" == "win32" ]]; then
    platform="windows"
  elif [[ "$OSTYPE" == "freebsd"* ]]; then
    echo "Freebsd this script does not support running on freebsd platform"
    exit 1
  else
    echo "Cannot detect the OS that you're running"
    exit 1
  fi

  export PLATFORM="$platform"
  export SUDO_PREFIX="sudo"
  [[ "$PLATFORM" == "windows" ]] && export SUDO_PREFIX=""
}

err_docker() {
  printf "\n\t${LIGHT_RED}*** Docker is not running ❌${NC} *** . Exiting the script.\n\n"
  exit 1
}

check_docker() {
  if [[ "$PLATFORM" == "windows" ]]; then
    if ! docker info > /dev/null 2>&1; then
      err_docker
    fi
  elif ! sudo docker info > /dev/null 2>&1; then 
   err_docker
  fi
}

