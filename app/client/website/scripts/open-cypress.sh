#!/bin/bash

set -euo pipefail

ENV="${ENV:-dev}"
script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)"

load_env() {
  local suffix=""
  [ "$ENV" != "dev" ] && suffix=".$ENV"

  set -a
  . "${script_dir}/../.env$suffix"
  set +a
}

[ -z "${CI:-}" ] && load_env
npm run cy:open
