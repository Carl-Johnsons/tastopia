/// <reference types="cypress-mochawesome-reporter" />

import "./commands";
import "cypress-mochawesome-reporter/register";
import installLogsCollector from "cypress-terminal-report/src/installLogsCollector";
import { generateTraceparent, generateBaggage } from "../../src/utils/telemetry";

installLogsCollector();

Cypress.on("uncaught:exception", err => {
  if (err.message.includes("Performance") || err.message.includes("NEXT_REDIRECT")) {
    return false;
  }
});

beforeEach(() => {
  const { traceId, traceparent } = generateTraceparent();
  const testTitle = Cypress.currentTest.title;

  cy.env(["GITHUB_RUN_ID", "ENV", "GRAFANA_FQDN"]).then(
    ({ GITHUB_RUN_ID, ENV, GRAFANA_FQDN }) => {
      const baggage = generateBaggage(testTitle, ENV || "dev", GITHUB_RUN_ID);

      cy.intercept("**", req => {
        req.headers["traceparent"] = traceparent;
        if (baggage) {
          req.headers["baggage"] = baggage;
        }
      });

      cy.addTestContext({
        title: "Distributed Trace ID",
        value: traceId
      });

      cy.log(`[OTEL] Trace ID: ${traceId}`);

      if (GRAFANA_FQDN) {
        const traceUrl = `https://${GRAFANA_FQDN}/a/grafana-exploretraces-app/explore?traceId=${traceId}`;

        cy.addTestContext({
          title: "Distributed Trace Link",
          value: traceUrl
        });

        cy.log(`[OTEL] Trace: ${traceUrl}`);
      }
    }
  );
});
