#!/bin/bash

set -euo pipefail

ENV="${ENV:-dev}"
script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)"

while getopts psh OPTS; do
  case $OPTS in
  s)
    opt_s=1
    ;;
  h)
    cat <<EOF

Usage: $0 [options]

Options:
  -s    Turn on headless testing mode.

  -h    Print this help text.

EOF
    exit 0
    ;;
  ?)
    echo "Unknown flag. Usage: $0 [-sh]"
    exit 1
    ;;
  esac
done

# Shift parsed options
shift $((OPTIND - 1))

args=()

if [ -z ${opt_s:-} ]; then
  args+=(--headed)
fi

load_env() {
  local suffix=""
  [ "$ENV" != "dev" ] && suffix=".$ENV"

  set -a
  . "${script_dir}/../.env$suffix"
  set +a
}

[ -z "${CI:-}" ] && load_env
npm run cy:run -- "${args[@]}"
