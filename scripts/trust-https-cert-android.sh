#!/usr/bin/env bash

PEM_FILE_NAME=cert.pem

dotnet dev-certs https -ep $PEM_FILE_NAME --format PEM

subjectHash=`openssl x509 -inform PEM -subject_hash_old -in $PEM_FILE_NAME | head -n 1`

openssl x509 -in $PEM_FILE_NAME -inform PEM -outform DER -out $subjectHash.0
adb root
adb push ./$subjectHash.0 /data/misc/user/0/cacerts-added/$subjectHash.0
adb shell "su 0 chmod 644 /data/misc/user/0/cacerts-added/$subjectHash.0"
adb reboot

rm ./$subjectHash.0 cert.pem 
