Feature: Login Successful

As a consumer of the API, when I submit valid credentials the API should return valid token.

Scenario: Login with valid credo
	Given I call the Login API with valid credentials
	| email              | password         |
	| eve.holt@reqres.in | Y2l0eXNsaWNrYQ== |
	Then the API responds with a valid token
