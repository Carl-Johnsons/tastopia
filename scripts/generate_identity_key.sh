#!/bin/sh

PRIVATE_KEY="private_key.pem"
PUBLIC_KEY="public_key.pem"
KEY_BITS=2048

openssl genpkey -algorithm RSA -out "$PRIVATE_KEY" -pkeyopt rsa_keygen_bits:"$KEY_BITS"
openssl rsa -pubout -in "$PRIVATE_KEY" -out "$PUBLIC_KEY"

echo "RSA key pair generated successfully! :)"

mv "$PRIVATE_KEY" ./solutions/IdentitySolution/DuendeIdentityServer
mv "$PUBLIC_KEY" ./solutions/IdentitySolution/DuendeIdentityServer