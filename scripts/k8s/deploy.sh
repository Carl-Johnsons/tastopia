project_root=$(pwd)

# Exit on failure
set -e

cd ./k8s

# Declare secret
cd "$project_root"

kubectl create secret generic global-secret \
	--from-env-file=.env.production
kubectl create secret generic identity-api-secret \
	--from-env-file=app/server/IdentityService/.env.production
kubectl create secret generic user-api-secret \
	--from-env-file=app/server/UserService/.env.production
kubectl create secret generic recipe-api-secret \
	--from-env-file=app/server/RecipeService/.env.production
kubectl create secret generic notification-api-secret \
	--from-env-file=app/server/NotificationService/.env.production
kubectl create secret generic upload-api-secret \
	--from-env-file=app/server/UploadFileService/.env.production
kubectl create secret generic tracking-api-secret \
	--from-env-file=app/server/TrackingService/.env.production
kubectl create secret generic signalr-secret \
	--from-env-file=app/server/SignalRService/.env.production
kubectl create secret generic api-gateway-secret \
	--from-env-file=app/server/APIGateway/.env.production
kubectl create secret generic ingredient-predict-api-secret \
	--from-env-file=app/server/IngredientPredictService/.env.production
echo ""
kubectl create secret tls identity-api-tls \
	--cert=./ssl/certs/identity.crt \
	--key=./ssl/private-key/identity.key
kubectl create secret tls user-api-tls \
	--cert=./ssl/certs/user.crt\
	--key=./ssl/private-key/user.key
kubectl create secret tls recipe-api-tls \
	--cert=./ssl/certs/recipe.crt\
	--key=./ssl/private-key/recipe.key
kubectl create secret tls notification-api-tls \
	--cert=./ssl/certs/notification.crt\
	--key=./ssl/private-key/notification.key
kubectl create secret tls tracking-api-tls \
	--cert=./ssl/certs/tracking.crt\
	--key=./ssl/private-key/tracking.key
kubectl create secret tls signalr-tls \
	--cert=./ssl/certs/signalr.crt\
	--key=./ssl/private-key/signalr.key
kubectl create secret tls api-gateway-tls \
	--cert=./ssl/certs/gateway.crt\
	--key=./ssl/private-key/gateway.key
echo ""

# Apply file .yaml
cd ./k8s

# kubectl apply -f deployments -f services
kubectl apply \
  -f ./deployments/identity-api.yaml -f ./services/identity-api.yaml \
  -f ./deployments/postgres.yaml -f ./services/postgres.yaml

cd "$project_root"
