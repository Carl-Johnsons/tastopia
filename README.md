# Tastopia

Using AI to suggest dishes based on ingredients

Commit rule:

-   Backend related: `be: `
-   Frontend related: `fe: `
-   AI related: `ai: `
-   DevOps related: `do: `
-   General: `g: `
-   Document related: `doc: `

## Action:

-   feature
-   refactor
-   fix
-   update

### Example:

`be: feature init post service`

## Back-end installation guide

Ensure that you have: 
- Docker
- Dotnet Sdk 8.0 and Asp.Net Core 8.0
  - On windows: Run `winget install --id=Microsoft.DotNet.SDK.8 -e`.
  - On MacOS: Download the installation file on [this website](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
  - When running the command `dotnet --list-runtimes`, it should list two items (ignore the x):
    - Microsoft.AspNetCore.App 8.0.x
    - Microsoft.NETCore.App 8.0.x
- Node.js
- Download google-service.json from Firebase project setting. Then put it in the `./app/client/mobile` directory.
- Download google credential from Google cloud then put it into the EmailWorker service's root path: `./app/server/NotificationService/src/EmailWorker/`.

0. Ensure that docker is running
1. Run this line in Git Bash or a regular terminal if you use MacOS/Linux:
``` bash
./runScripts
```
2. Choose the option `Generate SSL certificate`
3. Choose the option `Setup back-end`
4. After the setup is done, choose the option `Run all services`
5. Enjoy 💃✨

