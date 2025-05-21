project_root=$(pwd)

cd ./k8s

# Declare secret
cd "$project_root"

kubectl create secret generic global-secret \
	--from-env-file=.env.production
kubectl create secret generic identity-api-secret \
	--from-env-file=app/server/IdentityService/.env.production
kubectl create secret generic user-api-secret \
	--from-env-file=app/server/UserService/.env.production
echo ""
kubectl create secret tls identity-api-tls \
	--cert=./ssl/certs/identity.crt \
	--key=./ssl/private-key/identity.key
kubectl create secret tls user-api-tls \
	--cert=./ssl/certs/user.crt\
	--key=./ssl/private-key/user.key

echo ""

# Apply file .yaml
cd ./k8s

kubectl apply -f deployments -f services

cd "$project_root"
