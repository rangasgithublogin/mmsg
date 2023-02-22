Feature: Get a Specific User

As a consumer of the API, when I want the details of a specific User then the api should return their info

Scenario: Return Single User
	Given I request the info of user id 2
	Then the API response contains the details of user id 2

Scenario: A 404 is returned when the given user is not found
	Given I request the info of user id 23
	Then a 404 Not Found is retured