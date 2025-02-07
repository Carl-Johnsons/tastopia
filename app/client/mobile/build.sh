#!/bin/bash
echo "remove cache file ..."
rm -rf /tmp/metro-*
rm -rf /tmp/$USER/eas-build-local-nodejs
rm -rf ~/.gradle/caches/
rm -rf ~/.gradle/daemon/
echo "proceed to build"
EAS_NO_VCS=1 EAS_PROJECT_ROOT=.. npx eas build --platform android --profile simulator --local
