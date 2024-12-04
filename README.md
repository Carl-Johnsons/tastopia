# Tastopia

Using AI to suggest dishes based on ingredients

Commit rule:

-   Backend related: `be: `
-   Frontend related: `fe: `
-   AI related: `ai: `
-   DevOps related: `do: `
-   General: `g: `

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
- Dotnet Sdk >= 8.0
  - On windows: Run `winget install --id=Microsoft.DotNet.SDK.8 -e`
  - On MacOS: Download the installation file on [this website](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Node.js

0. Ensure that docker is running
1. Run this line in Git Bash or a regular terminal if you use MacOS/Linux:
``` bash
./runScripts
```
2. Choose the option `setup_backend`
3. After the setup is done, choose the option `run_all_services`
4. Enjoy 💃✨
