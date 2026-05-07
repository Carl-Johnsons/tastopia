#!/bin/bash

set -euo pipefail

script_dir=$(cd -- $(dirname -- "${BASH_SOURCE[0]}") && pwd)

if ! command -v gh >/dev/null 2>&1; then
    printf "\n\t*** ${LIGHT_RED}"gh" not found. Please install github cli${NC} ***\n\n"
    exit 1
fi


container_opts=()

if [ "$PLATFORM" = "linux" ]; then
  container_opts+=(
    --container-options
    "--group-add $(stat -c %g /var/run/docker.sock)"
  )
fi

act workflow_dispatch \
  -s GH_TOKEN="$(gh auth token)" \
  --secret-file "$script_dir/../../../.secrets" \
  -e event.json \
  "${container_opts[@]}"
