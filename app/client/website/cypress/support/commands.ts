/// <reference types="cypress" />

Cypress.Commands.add("loginByUi", () => {
  cy.visit("/");
  cy.contains("button", /Sign in/i).click();

  cy.env(["E2E_TEST_USERNAME", "E2E_TEST_PASSWORD", "DUENDE_IDS6_ISSUER"]).then(
    ({ E2E_TEST_USERNAME, E2E_TEST_PASSWORD, DUENDE_IDS6_ISSUER }) => {
      cy.location("origin", { timeout: 30000 }).should("eq", DUENDE_IDS6_ISSUER);
      cy.origin(
        DUENDE_IDS6_ISSUER,
        { args: { E2E_TEST_USERNAME, E2E_TEST_PASSWORD, DUENDE_IDS6_ISSUER } },
        ({ E2E_TEST_USERNAME, E2E_TEST_PASSWORD, DUENDE_IDS6_ISSUER }) => {
          cy.get("h1").contains("Welcome to");
          cy.get('input[placeholder="Your email or phone number"]').type(
            E2E_TEST_USERNAME
          );
          cy.get('input[placeholder="Password"]').first().type(E2E_TEST_PASSWORD);
          cy.contains("button", /^Login$/).click();
          cy.location("origin").then(origin => {
            if (origin === DUENDE_IDS6_ISSUER) {
              cy.get("body").then($body => {
                if ($body.text().toLowerCase().includes("your new password")) {
                  cy.get('input[placeholder="Password"]')
                    .first()
                    .clear()
                    .type(E2E_TEST_PASSWORD);
                  cy.get('input[placeholder="Retype password"]').type(E2E_TEST_PASSWORD);
                  cy.contains("button", /^Change Password$/).click();
                }
              });
            }
          });
        }
      );
    }
  );

  cy.url().should("match", /\/en\/(recipes|statistics|admins)/);
});

Cypress.Commands.add("logoutByUi", () => {
  cy.visit("/en/admins/me");
  cy.get('[data-testid="sign-out-btn"]').click();
  cy.contains("button", /Sign in with/i).should("be.visible");
});

declare global {
  namespace Cypress {
    interface Chainable {
      loginByUi(): Chainable<void>;
      logoutByUi(): Chainable<void>;
    }
  }
}

export {};
