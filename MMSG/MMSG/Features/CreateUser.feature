Feature: Create User

As a consumer of the API, when I submit the payload for a new User then the api should create a new user in the system

Scenario: Create New User
	Given I submit the info of new user
	| name   | job    |
	| Walker | Singer |
	Then the API should return the new user's info
