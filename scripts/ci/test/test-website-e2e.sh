#!/bin/bash

set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
repo_root="$(cd -- "$script_dir/../../.." &> /dev/null && pwd)"

. "$script_dir/../lib/server.sh"

ENV="${ENV:-dev}"

: "${E2E_TEST_USERNAME:?E2E_TEST_USERNAME is required}"
: "${E2E_TEST_PASSWORD:?E2E_TEST_PASSWORD is required}"

load_env() {
  local suffix=""

  if [ "$ENV" != "dev" ]; then
    suffix=".$ENV"
  fi

  set -a
  . "$repo_root/.env$suffix"
  set +a
}

wait_for_website() {
  local endpoint

  if ! endpoint="$(get_client_base_url)"; then
    echo "Failed to get website endpoint"
    exit 1
  fi

  local timeout=120
  local interval=2
  local elapsed=0

  until curl -fsL --connect-timeout 2 --max-time 5 "$endpoint" >/dev/null; do
    elapsed=$((elapsed + interval))

    if [ "$elapsed" -ge "$timeout" ]; then
      echo "Timed out waiting for website"
      exit 1
    fi

    echo "Waiting for website..."
    sleep "$interval"
  done

  echo "Website is online"
}

load_env
bash "$script_dir/wait-for-backend.sh"
wait_for_website

export WEBSITE_URL="$(get_client_base_url)"
export DUENDE_IDS6_ISSUER="$(get_duende_ids6_issuer)"
export TEST_USERNAME="$E2E_TEST_USERNAME"
export TEST_PASSWORD="$E2E_TEST_PASSWORD"

cd "$repo_root/app/client/website"
echo "Running website E2E tests..."
npm run cy:run
