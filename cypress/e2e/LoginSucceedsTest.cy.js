// describe('template spec', () => {
//   it('passes', () => {
//     cy.visit('https://example.cypress.io')
//   })
// })
describe('Login Functionality: login succeeds', () => {
  beforeEach(() => {
    // Visit the login page before each test
    cy.visit('http://localhost:3000');
  });

  it('should receive an OK in the backend', () => {
    const validUsername = 'Jaden';
    const validPassword = 'Jaden1';

    // Intercept the backend API call for login
    cy.intercept('POST', 'http://localhost:5257/api/Auth/login', {
      statusCode: 200,
    }).as('loginRequest2');

    // Perform the login action
    cy.get('#username').type(validUsername);
    cy.get('#password').type(validPassword);
    cy.get('button[aria-label="Log in"]').click();

    // Wait for the backend API call to complete
    cy.wait('@loginRequest2', { timeout: 5000 }).then((interception) => {
      // Assertions on the intercepted request and response for a successful login
      expect(interception.request.method).to.equal('POST');
      expect(interception.response.statusCode).to.equal(200);
    });
  });
});


