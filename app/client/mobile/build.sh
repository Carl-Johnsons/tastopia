#!/bin/bash

ENV="dev"
export EAS_USE_CACHE=1
export EAS_NO_VCS=1 
export EAS_PROJECT_ROOT=..

while getopts e:h OPTS; do
  case $OPTS in
    e) 
      if [ "$OPTARG" != "dev" ] && [ "$OPTARG" != "staging" ] && [ "$OPTARG" != "production" ]; then
        echo 'Only "dev", "staging" or "production" is allowed as value of -e flag.'
        exit 1
      fi

      ENV="$OPTARG"
      ;;
    h) cat <<EOF

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

load_env() {
  local suffix="" 

  if [ "$ENV" != "dev" ]; then
    suffix=".$ENV"
  fi

  echo "Loading .env$suffix file..."
  set -a
  . ".env$suffix"
  EXPO_PUBLIC_API_GATEWAY_HOST="${API_GATEWAY_HOST:-$EXPO_PUBLIC_API_GATEWAY_HOST}"
  EXPO_PUBLIC_API_GATEWAY_PORT="${API_GATEWAY_PORT:-$EXPO_PUBLIC_API_GATEWAY_PORT}"
  EXPO_PUBLIC_API_GATEWAY_SCHEME="${API_GATEWAY_SCHEME:-$EXPO_PUBLIC_API_GATEWAY_SCHEME}"
  EXPO_PUBLIC_IDENTITY_DISCOVERY_URL="${IDENTITY_DISCOVERY_URL:-$EXPO_PUBLIC_IDENTITY_DISCOVERY_URL}"
  EXPO_PUBLIC_BUILD_ENV="$ENV"
  set +a
}

load_env
npm ci

echo "Backing up app.json..."
cp app.json app.json.bak
echo "Adding real google-services.json path to app.json..."
yq -i '.expo.android.googleServicesFile = strenv(PWD) + "/google-services.json"' app.json

echo "Removing cache file ..."
rm -rf /tmp/metro-*
# rm -rf /tmp/$USER/eas-build-local-nodejs
# rm -rf ~/.gradle/caches/
# rm -rf ~/.gradle/daemon/
# rm -rf ./android

echo "Building..."
time npx eas build \
  --platform android \
  --profile simulator \
  --local

echo "Restoring app.json..."
mv app.json.bak app.json
