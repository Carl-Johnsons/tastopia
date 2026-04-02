#!/bin/bash

pipx inject ansible-core requests
cd ansible
./run.build.sh
