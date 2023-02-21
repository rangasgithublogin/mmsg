Feature: Patch User

As a consumer of the API, when I submit the payload for a updating a User then the api should update only the given user info & return the response

Scenario: Update User's specific info
	Given I submit the request to update user's specific info
	| id  | name         | job    |
	| 999 | Walker Hayes | Singer |
	Then the API should return the updated user's info
