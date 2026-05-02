#!/bin/bash

set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
. "$script_dir/../lib/server.sh"

ENV="${ENV:-dev}"

while getopts e:h OPTS; do
  case $OPTS in
    e) 
      if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
        echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
        exit 1
      fi

      ENV="$OPTARG"
      ;;
    h) cat <<EOF

Usage: $0 [options]

Options:
  -e [environment]   
        Specify the environment to build, accepted values
        are "dev", "staging" or "production". If omitted, 
        the default value is "staging".

  -h    Print this help.

EOF
      exit 0
      ;;
    ?) 
      echo "Unknown flag. Usage: $0 [-e dev|staging|production]"
      exit 1
      ;;
  esac
done

shift $((OPTIND - 1))

load_env() {
  local script_dir=$(cd $(dirname "${BASH_SOURCE[0]}") && pwd)
  suffix="" 

  if [ "$ENV" != "dev" ]; then
    suffix=".$ENV"
  fi

  set -a
  . "$script_dir/../../../.env$suffix"
  set +a
  unset suffix
}

wait_for_server() {
  local endpoint

  if ! endpoint="$(get_api_endpoint)/health"; then
    echo "Failed to get API endpoint"
    exit 1
  fi

  local timeout=60
  local interval=1
  local count=0

  until curl -fsL --connect-timeout 2 --max-time 3 "$endpoint" | grep -q "Healthy"; do
    ((++count))
    
    if (( count * interval >= timeout )); then
      echo Timed out
      exit 1
    fi

    echo "Waiting for backend..."
    sleep $interval
  done

  echo "Server is online"
}

load_env
wait_for_server
