#!/usr/bin/env bash

set -e

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"

echo "Run setup scripts"
ansible-playbook -K "$script_dir/site-build-fe.yml"
