#!/bin/bash

# Exiting on errors
set -e 

while getopts p: opts
do
  case $opts in
    p) 
      pflag=1
      PASSWD=$OPTARG;;
    ?) echo "Invalid option. Usage: $0 -p <user_password>"
      exit 1;;
  esac
done

if [ -z "$pflag" ]; then
  echo "The script must be run with p flag"
  exit 1
fi

echo "Install ansible plugins"
ansible-playbook ./playbooks/init-requirements.yml

echo "Run setup scripts for production"
echo $PASSWD | ansible-playbook -K site.prod.yml
