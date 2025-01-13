#!/bin/bash

# Kill a port
kill_port() {
  local PID=$1
  
  if [[ "$PLATFORM" == "windows" ]]; then
    taskkill -F //PID $PID
  else
    kill $PID
  fi
}

# Check if a port was provided as an argument
if [ -z "$1" ]; then
  echo "Usage: $0 <ip:port>"
  exit 1
fi

if [[ "$PLATFORM" == "windows" ]]; then
  PORT=\[::\]:$1
  PID=$(netstat -ano | findstr "$PORT" | awk '{print $5}' | cut -d':' -f2)
else
  PORT=$1
  PID=$(lsof -t -i:$PORT)
fi

# Check if PID is found
if [ -n "$PID" ]; then
  echo "Killing process with PID $PID using port $PORT"
  kill_port $PID
else
  echo "No process found using port $PORT"
fi
