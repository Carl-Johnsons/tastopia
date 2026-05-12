describe("Tags data exploration", () => {
  beforeEach(() => {
    cy.loginByUi();
  });

  it("renders tag data table and opens create dialog", () => {
    cy.visit("/en/tags");

    cy.get('[data-testid="tags-search-input"]').should("be.visible").type("bacon");
    cy.get('[data-testid="tags-data-table"]').should("be.visible");
    cy.get('[data-testid="tags-create-btn"]').should("be.visible").click();
    cy.get('[data-testid="tags-create-dialog"]').should("be.visible");
  });
});
