#!/bin/bash

. ./scripts/lib.sh

echo -e "${INFO}Checking for vulnerable .NET packages...${NC}"

env -u PLATFORM dotnet list "app/server/APIGateway" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/Contract" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/IdentityService" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/NotificationService" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/RecipeService" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/SignalRService" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/TrackingService" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/UploadFileService" package --vulnerable --include-transitive
env -u PLATFORM dotnet list "app/server/UserService" package --vulnerable --include-transitive