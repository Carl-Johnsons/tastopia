#!/bin/bash

set -euo pipefail

ENV="${ENV:-dev}"

script_dir=$(cd -- $(dirname -- "${BASH_SOURCE[0]}") && pwd)
bucket_name="tastopia-builds"

while getopts e:h OPTS; do
  case $OPTS in
  e)
    if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
      echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
      exit 1
    fi

    ENV="$OPTARG"
    ;;
  h)
    cat <<EOF

Usage: $0 [options]

Options:
  -e [environment]   
        Specify the environment to build, accepted values
        are "dev", "staging" or "production". If omitted, 
        the default value is "staging".

  -h    Print this help.

EOF
    exit 0
    ;;
  ?)
    echo "Unknown flag. Usage: $0 [-e dev|staging|production]"
    exit 1
    ;;
  esac
done

shift $((OPTIND - 1))

commit=$(git log -n 1 --pretty=format:%H -- app/client/mobile | cut -c1-8)
file_name="build-$ENV-$commit.apk"

install_app() {
  adb -e install -r "$script_dir/../../../$file_name"
}

install_app
