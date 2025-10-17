#!/bin/bash

# This script installs and configures Jenkins + Nginx on Ubuntu

set -e  # Stop if any command fails
read -p "Enter the Jenkins server name (e.g., jenkins.server): " SERVER_NAME

apt update
apt install openjdk-17-jdk -y
java --version

wget -O /etc/apt/keyrings/jenkins-keyring.asc \
  https://pkg.jenkins.io/debian-stable/jenkins.io-2023.key
echo "deb [signed-by=/etc/apt/keyrings/jenkins-keyring.asc]" \
  https://pkg.jenkins.io/debian-stable binary/ | sudo tee \
  /etc/apt/sources.list.d/jenkins.list > /dev/null

apt update
apt install jenkins -y

systemctl start jenkins
systemctl enable jenkins
ufw allow 8080

# Config nginx
apt install nginx -y
cat <<EOF >  /etc/nginx/conf.d/$SERVER_NAME.conf
server {
    listen 80;

    server_name $SERVER_NAME;

    location / {
        proxy_pass http://$SERVER_NAME:8080;
        proxy_http_version 1.1;
        proxy_set_header Upgrade \$http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host \$host;
        proxy_cache_bypass \$http_upgrade;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
EOF

systemctl restart nginx

echo "✅ Jenkins is installed and accessible via http://$SERVER_NAME"