#!/usr/bin/env bash

set -e

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"

init_playbook="init-test-requirements.yml"
site_playbook="site-test.yml"

if [[ "$OSTYPE" == msys* ]] \
  || [[ "$OSTYPE" == cygwin* ]] \
  || [[ "$OSTYPE" == win32* ]]; then
  echo "Windows detected"
  init_playbook="init-test-requirements-windows.yml"
  site_playbook="site-test-windows.yml"
fi

echo "Install ansible plugins"
ansible-playbook "$script_dir/playbooks/$init_playbook"

echo "Run setup scripts"
ansible-playbook -K "$script_dir/$site_playbook"

