describe("Users data exploration", () => {
  beforeEach(() => {
    cy.loginByUi();
  });

  it("renders user data table and accepts search input", () => {
    cy.visit("/en/users");

    cy.get('[data-testid="users-search-input"]').should("be.visible").type("john");
    cy.get('[data-testid="users-data-table"]').should("be.visible");
  });
});
