Feature: List users
As a consumer of the API, when I call the api then it lists all users on a given page
Scenario: List Users on a given page
Given a call to list all users on page 2 is made
Then the API lists all users on the given page