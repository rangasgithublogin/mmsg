Feature: List users
As a consumer of the API, when I call the api then it lists all users on a given page
Scenario Outline: List Users on a given page
	Given a call to list all users on page 2 is made
	When the API lists all users on the given page
	Then verify that user <name> is found in the list
Examples:
	| name             |
	| Lindsay Ferguson |
	| Byron Fields     |
	| George Edwards   |