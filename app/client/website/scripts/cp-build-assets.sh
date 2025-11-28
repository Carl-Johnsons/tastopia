#!/usr/bin/env bash

set -e

ROOT_DIR="$(pwd)"
DST_DIR=".next/standalone"
SRC_PUBLIC="$ROOT_DIR/public"
SRC_STATIC="$ROOT_DIR/.next/static"

mkdir -p "$DST_DIR/public"
mkdir -p "$DST_DIR/.next/static"

echo "Copying \"public\" folder..."
cp -urf "$SRC_PUBLIC" "$DST_DIR"

echo "Copying \".next/static\" folder..."
cp -urf "$SRC_STATIC" "$DST_DIR/.next"
