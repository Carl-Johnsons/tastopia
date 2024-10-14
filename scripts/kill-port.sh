#!/bin/bash

# Check if a port was provided as an argument
if [ -z "$1" ]; then
  echo "Usage: $0 <ip:port>"
  exit 1
fi

# The port to search for is passed as the first argument
PORT=$1

# Find the process using the port and get the PID
PID=$(netstat -ano | findstr "$PORT" | awk '{print $5}' | cut -d':' -f2)

# Check if PID is found
if [ -n "$PID" ]; then
  echo "Killing process with PID $PID using port $PORT"
  # Kill the process, ensuring correct format
  taskkill -F //PID $PID
else
  echo "No process found using port $PORT"
fi
