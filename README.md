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

Ensure that you have these system dependencies installed:
- Docker
- Git Bash on Windows, or a regular terminal on macOS/Linux
- Download google-service.json from Firebase project setting. Then put it in the `./app/client/mobile` directory.
- Download google credential from Google cloud then put it into the EmailWorker service's root path: `./app/server/NotificationService/src/EmailWorker/`.

0. Ensure that docker is running
1. Run this line in Git Bash on Windows, or a regular terminal on macOS/Linux:
``` bash
./runScripts.sh
```
2. Choose the option `Set up project dependencies`.
3. Run `./runScripts.sh` again.
4. Choose the option `Pulling environment variables`.
   - You need the `INFISICAL_READ_TOKEN` from the Secrets tab to fetch secrets.
5. Choose the option `Setup back-end`.
6. After the setup is done, choose the option `Run all services`.
7. Enjoy 💃✨
