describe("Recipes data exploration", () => {
  beforeEach(() => {
    cy.loginByUi();
  });

  it("renders recipe data table and accepts search input", () => {
    cy.visit("/en/recipes");

    cy.get('[data-testid="recipes-search-input"]').should("be.visible").type("chicken");
    cy.get('[data-testid="recipes-data-table"]').should("be.visible");
  });
});
