#!/bin/bash

set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
repo_root="$(cd -- "$script_dir/../../.." &> /dev/null && pwd)"

. "$script_dir/../lib/server.sh"

website_env_file="$repo_root/app/client/website/.env"

upsert_env() {
  local key="${1:?key is required}"
  local value="${2-}"
  local file="${3:?file is required}"
  local tmp_file
  tmp_file="$(mktemp)"

  awk -v key="$key" -v value="$value" '
    BEGIN { updated = 0 }
    $0 ~ ("^" key "=") {
      print key "=" value
      updated = 1
      next
    }
    { print }
    END {
      if (updated == 0) {
        print key "=" value
      }
    }
  ' "$file" > "$tmp_file"

  mv "$tmp_file" "$file"
}

setup_website_env() {
  mkdir -p "$(dirname "$website_env_file")"
  touch "$website_env_file"

  upsert_env "NEXT_PUBLIC_API_GATEWAY_HOST" "$(get_api_fqdn)" "$website_env_file"
  upsert_env "NEXT_PUBLIC_API_GATEWAY_PORT" "$(get_api_port)" "$website_env_file"
  upsert_env "NEXT_PUBLIC_API_GATEWAY_SCHEME" "$(get_api_scheme)" "$website_env_file"
  upsert_env "NEXT_PUBLIC_CLIENT_BASE_URL" "$(get_client_base_url)" "$website_env_file"
  upsert_env "NEXT_PUBLIC_DUENDE_IDS6_ID" "$(get_duende_ids6_id)" "$website_env_file"
  upsert_env "NEXT_PUBLIC_DUENDE_IDS6_ISSUER" "$(get_duende_ids6_issuer)" "$website_env_file"
}

generate_public_env() {
  (
    cd "$repo_root"
    ./scripts/env/generate-public-env.sh dev
  )
}

setup_website_env
generate_public_env
