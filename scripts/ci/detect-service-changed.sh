#!/bin/bash

SERVICES_JSON='{
    "website": ["app/client/website"],
    "api-gateway": ["app/server/APIGateway"],
    "signalr": ["app/server/SignalRService"],
    "tracking-api": ["app/server/TrackingService"],
    "upload-api": ["app/server/UploadFileService"],
    "identity-api": ["app/server/IdentityService"],
    "notification-api": [
        "app/server/NotificationService/src/NotificationService.API",
        "app/server/NotificationService/src/NotificationService.Application",
        "app/server/NotificationService/src/NotificationService.Domain",
        "app/server/NotificationService/src/NotificationService.Infrastructure"
    ],
    "recipe-api": [
        "app/server/RecipeService/src/RecipeService.API",
        "app/server/RecipeService/src/RecipeService.Application",
        "app/server/RecipeService/src/RecipeService.Domain",
        "app/server/RecipeService/src/RecipeService.Infrastructure"
    ],
    "user-api": ["app/server/UserService"],
    "ingredient-predict-api": ["app/server/IngredientPredictService"],
    "email-worker": ["app/server/NotificationService/src/EmailWorker"],
    "sms-worker": ["app/server/NotificationService/src/SMSWorker"],
    "push-notification-worker": ["app/server/NotificationService/src/PushNotificationWorker"],
    "recipe-worker": ["app/server/RecipeService/src/RecipeWorker"]
}'

# Handle first successfully built commit
PREV=$(gh run list \
  --workflow '.github/workflows/ci.yaml' \
  --branch "$BRANCH" \
  --status success \
  --limit 1 \
  --json headSha -q '.[0].headSha' \
  || echo ""
)

# validate commit is reachable from current HEAD
if [[ -n "$PREV" ]] && ! git merge-base --is-ancestor "$PREV" HEAD; then
  echo "Invalid or unrelated commit, fallback"
  PREV=""
fi

if [ -z "$PREV" ]; then
  echo "No previous commit, build all services"
  services=$(echo $SERVICES_JSON | jq -r 'keys | join(" ")') 
  echo "services=$services"
  echo "services=$services" >> "$GITHUB_OUTPUT"
  exit 0
fi

# Handle changed service commit
CHANGED=$(git diff --name-only $PREV HEAD)
RESULT=""

for svc in $(echo $SERVICES_JSON | jq -r 'keys[]'); do
  for path in $(echo $SERVICES_JSON | jq -r ".\"$svc\"[]"); do
      if echo "$CHANGED" | grep -q "^$path"; then
        RESULT="${RESULT:+$RESULT }$svc"
        break
      fi
  done
done

if [ -z $RESULT ]; then
  echo "No services changed"
  exit 0
fi

echo "services=$RESULT"
echo "services=$RESULT" >> $GITHUB_OUTPUT
