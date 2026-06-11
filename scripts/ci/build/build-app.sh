#!/bin/bash

set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &> /dev/null && pwd)"
. "$script_dir/../lib/server.sh"

AWS_ACCESS_KEY_ID="${AWS_ACCESS_KEY_ID:?AWS_ACCESS_KEY_ID is required}"
AWS_SECRET_ACCESS_KEY="${AWS_SECRET_ACCESS_KEY:?AWS_SECRET_ACCESS_KEY is required}"
AWS_ENDPOINT_URL="${AWS_ENDPOINT_URL:?AWS_ENDPOINT_URL is required}"
AWS_DEFAULT_REGION="${AWS_DEFAULT_REGION:?AWS_DEFAULT_REGION is required}"
ENV="${ENV:-dev}"

bucket_name="tastopia-builds"

tag=""

while getopts e:ht: OPTS; do
  case $OPTS in
    e) 
      if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
        echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
        exit 1
      fi

      ENV="$OPTARG"
      ;;
    t) tag="$OPTARG" ;;
    h) cat <<EOF

Usage: $0 [options]

Options:
  -e [environment]   
        Specify the environment to build, accepted values
        are "dev", "staging" or "production". If omitted, 
        the default value is "staging".

  -t [tag]
        Explicitly specify the tag to use for the build.

  -h    Print this help.

EOF
      exit 0
      ;;
    ?) 
      echo "Unknown flag. Usage: $0 [-t tag] [-e dev|staging|production]"
      exit 1
      ;;
  esac
done

shift $((OPTIND - 1))

if [ -z "$tag" ]; then
  commit_hash=$(git log -n 1 --pretty=format:%H -- app/client/mobile | cut -c1-8)

  if [ "$ENV" = "dev" ]; then
    : "${PR_NUMBER:?PR_NUMBER env value is required for dev environment}"
    file_name="build-$ENV-$PR_NUMBER-$commit_hash.apk"
  else
    file_name="build-$ENV-$commit_hash.apk"
  fi
else
  file_name="build-${tag}.apk"
fi

is_build_exists() {
  if ! aws s3api head-bucket --bucket "$bucket_name" >/dev/null 2>&1; then
    return 1
  fi

  if aws s3api head-object \
    --bucket "$bucket_name" \
    --key "$file_name" \
    >/dev/null 2>&1; then
    return 0
  fi

  return 1
}

setup_env() {
  set -a
  API_GATEWAY_HOST="$(get_api_fqdn)"
  API_GATEWAY_PORT="$(get_api_port)"
  API_GATEWAY_SCHEME="$(get_api_scheme)"
  IDENTITY_DISCOVERY_URL="$(get_identity_discovery_url)"
  set +a
}

build_app() {
  cd app/client/mobile
  ./build.sh -e "$ENV"
  ls | grep "build.*.apk" | xargs -rI {} mv "{}" "$file_name"
}

upload_artifact() {
  if ! aws s3api head-bucket --bucket "$bucket_name" >/dev/null 2>&1; then
    aws s3 mb "s3://$bucket_name"
  fi

  aws s3 cp "$file_name" "s3://$bucket_name"
}

if ! is_build_exists; then
  setup_env
  build_app

  retry_threshold=60
  interval=1
  count=0

  while true; do
    ((++count))

    if (( count * interval >= retry_threshold )); then
      echo "Timed out while uploading"
      exit 1
    fi

    if upload_artifact; then
      echo "Uploaded $file_name"
      break
    fi

    echo "Upload failed, retrying..."
    sleep $interval
  done
else
  echo "Build exists, skipping..."
fi
