project_root=$(pwd)

docker run -d -p 9000:5000 --restart=always --name registry registry:2

cd ./k8s

# Push to local registry
# docker tag chat-application-api-gateway localhost:6000/chat-application-api-gateway
# docker tag chat-application-sql-server.configurator localhost:6000/chat-application-sql-server.configurator
# docker tag chat-application-identity-api localhost:6000/chat-application-identity-api
# docker tag chat-application-conversation-api localhost:6000/chat-application-conversation-api
# docker tag chat-application-post-api localhost:6000/chat-application-post-api
# docker tag chat-application-upload-api localhost:6000/chat-application-upload-api
# docker tag chat-application-websocket localhost:6000/chat-application-websocket
# docker tag chat-application-notification-api localhost:6000/chat-application-notification-api

# docker push localhost:6000/chat-application-api-gateway
# docker push localhost:6000/chat-application-identity-api
# docker push localhost:6000/chat-application-conversation-api
# docker push localhost:6000/chat-application-sql-server.configurator
# docker push localhost:6000/chat-application-post-api
# docker push localhost:6000/chat-application-upload-api
# docker push localhost:6000/chat-application-websocket
# docker push localhost:6000/chat-application-notification-api

# Declare secret
cd "$project_root"
echo -e ""
kubectl delete secret global-secret
echo -e ""

kubectl create secret generic global-secret \
	--from-env-file=.env.production
echo -e ""

# Apply file .yaml
cd ./k8s

kubectl apply -f deployments -f services

cd "$project_root"
