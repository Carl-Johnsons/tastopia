#!/bin/bash

set -euo pipefail

ENV="${ENV:-dev}"
FQDN_PREFIX="${HOST_PREFIX:+$HOST_PREFIX-}${ENV:+$ENV-}"
 
get_api_endpoint() {
  : "${API_GATEWAY_FQDN:?API_GATEWAY_FQDN is missing}"

  if [ "$ENV" = "dev" ]; then
    : "${PR_NUMBER:?PR_NUMBER is missing}"
  fi

  local endpoint

  if [ "$ENV" = "dev" ]; then
    endpoint="https://pr-$PR_NUMBER-${FQDN_PREFIX}$API_GATEWAY_FQDN"
  else 
    endpoint="https://${FQDN_PREFIX}$API_GATEWAY_FQDN"
  fi

  echo "$endpoint"
}

get_api_fqdn() {
  : "${API_GATEWAY_FQDN:?API_GATEWAY_FQDN is missing}"

  if [ "$ENV" = "dev" ]; then
    : "${PR_NUMBER:?PR_NUMBER is missing}"
  fi

  local fqdn

  if [ "$ENV" = "dev" ]; then
    fqdn="pr-$PR_NUMBER-${FQDN_PREFIX}$API_GATEWAY_FQDN"
  else 
    fqdn="${FQDN_PREFIX}$API_GATEWAY_FQDN"
  fi

  echo "$fqdn"
}

get_api_port() {
  local port="${API_GATEWAY_PORT:-443}"
  echo "$port"
}

get_api_scheme() {
  local scheme="${API_GATEWAY_SCHEME:-https}"
  echo "$scheme"
}

get_client_base_url() {
  : "${WEBSITE_FQDN:?WEBSITE_FQDN is missing}"

  if [ "$ENV" = "dev" ]; then
    : "${PR_NUMBER:?PR_NUMBER is missing}"
  fi

  local scheme="${WEBSITE_SCHEME:-https}"
  local fqdn

  if [ "$ENV" = "dev" ]; then
    fqdn="pr-$PR_NUMBER-${FQDN_PREFIX}$WEBSITE_FQDN"
  else
    fqdn="${FQDN_PREFIX}$WEBSITE_FQDN"
  fi

  echo "${scheme}://$fqdn"
}

get_identity_discovery_url() {
  : "${IDENTITY_FQDN:?IDENTITY_FQDN is missing}"

  if [ "$ENV" = "dev" ]; then
    : "${PR_NUMBER:?PR_NUMBER is missing}"
  fi

  local endpoint

  if [ "$ENV" = "dev" ]; then
    endpoint="https://pr-$PR_NUMBER-${FQDN_PREFIX}$IDENTITY_FQDN"
  else 
    endpoint="https://${FQDN_PREFIX}$IDENTITY_FQDN"
  fi

  echo "$endpoint"
}

get_duende_ids6_id() {
  : "${DUENDE_IDS6_ID:?DUENDE_IDS6_ID is missing}"
  echo "$DUENDE_IDS6_ID"
}

get_duende_ids6_issuer() {
  get_identity_discovery_url
}
