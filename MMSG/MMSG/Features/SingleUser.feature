Feature: Get a Specific User

As a consumer of the API, when I want the details of a specific User then the api should return their info

Scenario: Return Single User
	Given I request the info of user id 2
	Then the API returns the details of user id 2
