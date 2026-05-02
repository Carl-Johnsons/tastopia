#!/usr/bin/env bash

set -e

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"

echo "Install ansible plugins"
ansible-playbook "$script_dir/playbooks/init-test-requirements.yml"

echo "Run setup scripts"
ansible-playbook -K "$script_dir/site-test.yml"
