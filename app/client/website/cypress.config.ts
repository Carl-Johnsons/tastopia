import { defineConfig } from "cypress";

export default defineConfig({
  allowCypressEnv: false,
  env: {
    DUENDE_IDS6_ISSUER: process.env.DUENDE_IDS6_ISSUER ?? "http://localhost:5001",
    E2E_TEST_USERNAME: process.env.E2E_TEST_USERNAME,
    E2E_TEST_PASSWORD: process.env.E2E_TEST_PASSWORD
  },
  e2e: {
    baseUrl: process.env.WEBSITE_URL ?? "http://localhost:3000",
    specPattern: "cypress/e2e/**/*.cy.ts",
    setupNodeEvents(on, config) {
      require("cypress-mochawesome-reporter/plugin")(on);
      require("cypress-terminal-report/src/installLogsPrinter")(on, {
        outputRoot: `${config.projectRoot}/cypress/reports/logs`,
        outputTarget: {
          "terminal-report.txt": "txt",
          "terminal-report.json": "json"
        }
      });
    }
  },
  reporter: "cypress-mochawesome-reporter",
  reporterOptions: {
    charts: true,
    embeddedScreenshots: true,
    inlineAssets: true,
    saveAllAttempts: false,
  },
});
