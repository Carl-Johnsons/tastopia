#!/bin/bash

set -euo pipefail
# ansible already installed in ubuntu image
# To prevent virtual environment error, act must create vm to install ansible

if [ "${ACT:-}" = "true" ]; then
    echo "Running under act"
    python3 -m venv .venv

    source .venv/bin/activate

    pip install ansible requests
    cd ansible
    bash run-build.sh
else
    pipx inject ansible-core requests
    cd ansible
    ./run-build.sh
fi
