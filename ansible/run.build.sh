#!/usr/bin/env bash

set -e

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"

echo "Install ansible plugins"
ansible-playbook "$SCRIPT_DIR/playbooks/init-requirements.yml"

echo "Run setup scripts"
ansible-playbook -K -vvv "$SCRIPT_DIR/site.build.yml"
