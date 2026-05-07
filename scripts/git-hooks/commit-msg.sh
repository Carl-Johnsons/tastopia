#!/bin/bash

MSG_FILE=$1
MSG=$(cat "$MSG_FILE")

# Allow git fixup!/squash!/amend! commits
if [[ $MSG =~ ^(fixup|squash|amend)!\ .+ ]]; then
  exit 0
fi

# Allowed types
TYPES="be|fe|ai|do|g|feat|fix|chore|doc|style|refactor|perf|test|build|ci|revert|config|wip"

# Pattern: type(scope?): message
REGEX="^($TYPES)(\(.+\))?: .+"

if [[ ! $MSG =~ $REGEX ]]; then
  echo "❌ Invalid commit message format!"
  echo ""
  echo "Expected format:"
  echo "  <type>(optional-scope): <message>"
  echo ""
  echo "Allowed types:"
  echo "  be, fe, ai, do, g,"
  echo "  feat, fix, chore, doc, style, refactor,"
  echo "  perf, test, build, ci, revert, config, wip"
  echo ""
  echo "Examples:"
  echo "  be(auth): add login with Google"
  echo "  fe(auth): add login page"
  echo "  do: init ci/cd pipeline"
  echo "  feat(auth): add login with Google"
  echo "  fix(api): handle null response"
  echo "  doc: update README"
  echo ""
  exit 1
fi

exit 0
