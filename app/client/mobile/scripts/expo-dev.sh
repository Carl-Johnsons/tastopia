#!/usr/bin/env bash

cd "$(dirname "$0")/.."

stop=false

trap 'stop=true' INT TERM

while true; do
    echo "Starting Expo..."

    npx expo start --android

    if $stop; then
        break
    fi

    echo "Expo stopped. Restarting..."
    sleep 1
done