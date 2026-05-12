describe("User reports data exploration", () => {
  beforeEach(() => {
    cy.loginByUi();
  });

  it("renders user reports table and accepts search input", () => {
    cy.visit("/en/reports/users");

    cy.get('[data-testid="report-users-search-input"]').should("be.visible").type("spam");
    cy.get('[data-testid="report-users-data-table"]').should("be.visible");
  });
});
