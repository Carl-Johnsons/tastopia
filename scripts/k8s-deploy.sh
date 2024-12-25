project_root=$(pwd)

docker run -d -p 6000:5000 --restart=always --name registry registry:2

cd ./kubernetes-configs

# Push to local registry
docker tag chat-application-api-gateway localhost:6000/chat-application-api-gateway
docker tag chat-application-sql-server.configurator localhost:6000/chat-application-sql-server.configurator
docker tag chat-application-identity-api localhost:6000/chat-application-identity-api
docker tag chat-application-conversation-api localhost:6000/chat-application-conversation-api
docker tag chat-application-post-api localhost:6000/chat-application-post-api
docker tag chat-application-upload-api localhost:6000/chat-application-upload-api
docker tag chat-application-websocket localhost:6000/chat-application-websocket
docker tag chat-application-notification-api localhost:6000/chat-application-notification-api

docker push localhost:6000/chat-application-api-gateway
docker push localhost:6000/chat-application-identity-api
docker push localhost:6000/chat-application-conversation-api
docker push localhost:6000/chat-application-sql-server.configurator
docker push localhost:6000/chat-application-post-api
docker push localhost:6000/chat-application-upload-api
docker push localhost:6000/chat-application-websocket
docker push localhost:6000/chat-application-notification-api

# Declare secret
cd "$project_root"
echo -e ""
kubectl delete secret general-secret \
	api-gateway-secret \
	identity-api-secret \
	conversation-api-secret \
	post-api-secret \
	upload-api-secret \
	websocket-secret \
	notification-api-secret
echo -e ""

kubectl create secret generic general-secret \
	--from-env-file=.env.production
kubectl create secret generic api-gateway-secret \
	--from-env-file=./solutions/APIGatewaySolution/src/APIGateway/.env
kubectl create secret generic identity-api-secret \
	--from-env-file=./solutions/IdentitySolution/DuendeIdentityServer/.env
kubectl create secret generic conversation-api-secret \
	--from-env-file=./solutions/ConversationSolution/src/ConversationService.API/.env
kubectl create secret generic post-api-secret \
	--from-env-file=./solutions/PostSolution/src/PostService.API/.env
kubectl create secret generic upload-api-secret \
	--from-env-file=./solutions/UploadFileSolution/src/UploadFileService.API/.env
kubectl create secret generic websocket-secret \
	--from-env-file=./solutions/ChatHubSolution/src/ChatHub/.env
kubectl create secret generic notification-api-secret \
	--from-env-file=./solutions/NotificationSolution/src/NotificationService.API/.env
echo -e ""

# Apply file .yaml
cd ./kubernetes-configs

kubectl apply -f deployments -f services

cd "$project_root"
