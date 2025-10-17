#!/bin/bash

CERT_PATH=./ssl/certs
CSR_PATH=./ssl/csr
KEY_PATH=./ssl/private-key

mkdir -p "$CERT_PATH" "$CSR_PATH" "$KEY_PATH"

openssl genrsa -out $KEY_PATH/self-root-ca.key 4096
openssl req -x509 -new -nodes -key $KEY_PATH/self-root-ca.key -sha256 -days 730 -out $CERT_PATH/self-root-ca.crt -subj "/CN=RootCA"

gen_cert_for_service() {
    file_name=$1
    service_name=$2

    openssl genrsa -out $KEY_PATH/$file_name.key 2048
    openssl req -new -key $KEY_PATH/$file_name.key -out $CSR_PATH/$file_name.csr -subj "/CN=$service_name"
    openssl x509 -req -in $CSR_PATH/$file_name.csr -CA $CERT_PATH/self-root-ca.crt -CAkey $KEY_PATH/self-root-ca.key -CAcreateserial -out $CERT_PATH/$file_name.crt -days 500 -sha256
}

gen_cert_for_service gateway api-gateway
gen_cert_for_service identity identity-api
gen_cert_for_service user user-api
gen_cert_for_service recipe recipe-api
gen_cert_for_service notification notification-api
gen_cert_for_service upload upload-api
gen_cert_for_service tracking tracking-api
gen_cert_for_service ingredient-predict ingredient-predict-api
gen_cert_for_service signalr signalr-hub
gen_cert_for_service recipe-worker recipe-worker
