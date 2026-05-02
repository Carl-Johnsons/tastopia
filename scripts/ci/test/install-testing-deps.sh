#!/bin/bash

set -euo pipefail

AWS_ACCESS_KEY_ID="${AWS_ACCESS_KEY_ID:?AWS_ACCESS_KEY_ID is required}"
AWS_SECRET_ACCESS_KEY="${AWS_SECRET_ACCESS_KEY:?AWS_SECRET_ACCESS_KEY is required}"
AWS_ENDPOINT_URL="${AWS_ENDPOINT_URL:?AWS_ENDPOINT_URL is required}"
AWS_DEFAULT_REGION="${AWS_DEFAULT_REGION:?AWS_DEFAULT_REGION is required}"
ENV="${ENV:-dev}"
MAESTRO_VERSION="${MAESTRO_VERSION:-1.1.12}"
APPIUM_UIAUTOMATOR2_VERSION="${APPIUM_UIAUTOMATOR2_VERSION:-9.11.2}"

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

install_maestro() {
  if command -v maestro-runner >/dev/null 2>&1 && maestro-runner --version | grep -q "$MAESTRO_VERSION"; then
    echo "Maestro v$MAESTRO_VERSION is already exist, skip downloading..."
    return 0
  fi

  echo "Downloading maestro-runner v$MAESTRO_VERSION..."
  local download_url="https://github.com/PhenChua29/maestro-runner/releases/download/v$MAESTRO_VERSION/maestro-runner-linux-amd64"
  local maestro_runner_home="$HOME/.maestro-runner"
  local bin_dir="$maestro_runner_home/bin"
  local file="$bin_dir/maestro"
  local drivers_dir="$maestro_runner_home/drivers/android"

  mkdir -p "$bin_dir"
  curl -fsSL "$download_url" > "$file"
  chmod +x "$file"
  echo "$HOME/.maestro-runner/bin" >> $GITHUB_PATH

  install_maestro_drivers
}

install_maestro_drivers() {
  local maestro_runner_home="$HOME/.maestro-runner"
  local android_drivers_dir="$maestro_runner_home/drivers/android"

  mkdir -p "$android_drivers_dir" 
  cd "$android_drivers_dir"

  local repo_link='https://github.com/appium/appium-uiautomator2-server'
  local driver_link="${repo_link}/releases/download/v${APPIUM_UIAUTOMATOR2_VERSION}/appium-uiautomator2-server-v${APPIUM_UIAUTOMATOR2_VERSION}.apk"
  local test_driver_link="${repo_link}/releases/download/v${APPIUM_UIAUTOMATOR2_VERSION}/appium-uiautomator2-server-debug-androidTest.apk"

  if [ ! -f "$android_drivers_dir/appium-uiautomator2-server-v*.apk" ]; then
    curl -fsSLO "$driver_link"
  fi

  if [ ! -f "$android_drivers_dir/appium-uiautomator2-server-debug-androidTest.apk" ]; then
    curl -fsSLO "$test_driver_link"
  fi
}

fetch_artifact() {
  aws s3 cp "s3://$bucket_name/$file_name" "$script_dir/../../../$file_name"
}

install_maestro
fetch_artifact
