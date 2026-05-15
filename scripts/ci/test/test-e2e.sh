#!/bin/bash

set -euo pipefail

: "${E2E_TEST_USERNAME:?E2E_TEST_USERNAME is required}"
: "${E2E_TEST_PASSWORD:?E2E_TEST_PASSWORD is required}"
: "${ENV:?ENV is required for testing}"

script_dir=$(cd -- $(dirname -- "${BASH_SOURCE[0]}") && pwd)
mobile_dir="$script_dir/../../../app/client/mobile"

$script_dir/wait-for-backend.sh

cd "$mobile_dir"
echo Running tests...
npm run test
