Feature: Update User

As a consumer of the API, when I submit the payload for a updating a User then the api should update the user info & return the response

Scenario: Update a User
	Given I submit the request to update user
	| id  | name         | job            |
	| 999 | Walker Hayes | Country Singer |
	Then the API should return the updated user's info
