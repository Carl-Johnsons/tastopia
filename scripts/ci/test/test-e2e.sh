#!/bin/bash

set -euo pipefail

: "${E2E_TEST_USERNAME:?E2E_TEST_USERNAME is required}"
: "${E2E_TEST_PASSWORD:?E2E_TEST_PASSWORD is required}"
: "${ENV:?ENV is required for testing}"
export ENV="${ENV}"
export GITHUB_RUN_ID="${GITHUB_RUN_ID:-}"

script_dir=$(cd -- $(dirname -- "${BASH_SOURCE[0]}") && pwd)
mobile_dir="$script_dir/../../../app/client/mobile"

$script_dir/wait-for-backend.sh

cd "$mobile_dir"

LOGCAT_PID=""
stop_logcat() {
  if [ -n "$LOGCAT_PID" ]; then
    kill "$LOGCAT_PID" 2>/dev/null || true
    wait "$LOGCAT_PID" 2>/dev/null || true
    LOGCAT_PID=""
  fi
}
trap stop_logcat EXIT

if command -v adb &> /dev/null; then
  mkdir -p logs
  adb logcat -c || true
  adb logcat -P "" || true
  adb logcat -v time > logs/raw-logcat.log 2>&1 &
  LOGCAT_PID=$!
fi

echo "Running tests..."
set +e
npm run test
TEST_EXIT_CODE=$?
set -e

if command -v adb &> /dev/null; then
  stop_logcat

  if [ -s logs/raw-logcat.log ]; then
    grep "\[OTEL_TRACE\]" logs/raw-logcat.log > logs/otel-traces.log || true
  else
    adb logcat -d | grep "\[OTEL_TRACE\]" > logs/otel-traces.log || true
  fi

  if [ -n "${GITHUB_STEP_SUMMARY:-}" ] && [ -s logs/otel-traces.log ]; then
    {
      echo "### Mobile E2E OTEL Traces"
      echo "| Test | Trace ID |"
      echo "|---|---|"
      awk -F' ' '{
        test=""; traceId="";
        for(i=1; i<=NF; i++) {
          if ($i ~ /^test=/) test=substr($i, 6);
          if ($i ~ /^traceId=/) traceId=substr($i, 9);
        }
        if (traceId != "") {
          printf "| `%s` | `%s` |\n", (test != "" ? test : "Unknown"), traceId;
        }
      }' logs/otel-traces.log | sort -u
    } >> "$GITHUB_STEP_SUMMARY"
  fi
fi

exit "$TEST_EXIT_CODE"
