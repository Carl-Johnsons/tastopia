#!/usr/bin/env bash

set -e

echo "Install ansible plugins"
ansible-playbook ./playbooks/init-requirements.yml

echo "Run setup scripts"
ansible-playbook -K site.prod.yml
