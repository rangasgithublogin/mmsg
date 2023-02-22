# SpecFlow Test Automation Demo
- A sample test automation project to test API's using SpecFlow & .Net 6
- Test Features added for List Users, GET Single User, Post, Put & Patch
- Single User test inclues the 404 not found, a negative test
- The List Users test is a data-driven test for different users & the JSON response is parsed to findout the availability of each user
- To demonstrate usage of vital info like password etc effective Login test feature uses a encoded password
- From powershell prompt run 'dotnet test' to execute all the tests
- **NOTE**: I have used MSTest project as I faced some env issues when using SpecFlow project
