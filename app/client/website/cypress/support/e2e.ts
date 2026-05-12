import "./commands";
import "cypress-mochawesome-reporter/register";
import installLogsCollector from "cypress-terminal-report/src/installLogsCollector";

installLogsCollector();

Cypress.on("uncaught:exception", err => {
  if (err.message.includes("Performance") || err.message.includes("NEXT_REDIRECT")) {
    return false;
  }
});
