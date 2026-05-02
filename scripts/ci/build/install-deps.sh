#!/bin/bash

set -euo pipefail

pipx inject ansible-core requests
cd ansible
./run-build.sh
