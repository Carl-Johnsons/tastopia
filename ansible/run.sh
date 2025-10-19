#!/bin/bash

# Exiting on errors
set -e 

echo "Init"
ansible-playbook ./playbooks/init.yml

echo "Install ansible plugins"
ansible-playbook ./playbooks/init-requirements.yml

echo "Run setup scripts"
ansible-playbook -K site.yml
