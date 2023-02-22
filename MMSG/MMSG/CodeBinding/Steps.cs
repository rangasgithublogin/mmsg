using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;
using RestSharp;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Security.Principal;
using RestSharp.Serializers;
using System.Linq;

namespace MMSG.CodeBinding
{
    class user
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }

    class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }

    class Users
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public IList<user> data { get; set; }
        public Support support { get; set; }
    }

    class userJson
    {
        public user data { get; set; }
    }

    class Person
    {
        public string name { get; set; }
        public string job { get; set; }
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }

    class loginCredo
    {
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }

    [Binding]
    public class Steps
    {
        private readonly ScenarioContext _scenarioContext;
        RestSharp.RestClient client = new RestClient("https://reqres.in");

        public Steps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a call to list all users on page (.*) is made")]
        public void GivenACallToListAllUsersOnPageIsMade(int pageNumber)
        {
            var request = new RestRequest("api/users");
            request.AddParameter("page", pageNumber);
            var response = client.Get(request);
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.StatusCode.ToString());
            _scenarioContext["pageNumber"] = pageNumber;
            _scenarioContext["getUsersResp"] = response.Content;
        }

        [When(@"the API lists all users on the given page")]
        [Then(@"the API lists all users on the given page")]
        public void ThenTheAPIListsAllUsersOnTheGivenPage()
        {
            string response = (string) _scenarioContext["getUsersResp"];
            Assert.IsNotNull(response);
            Users users = JsonConvert.DeserializeObject<Users>(response);
            Assert.AreEqual(users.page, (int)_scenarioContext["pageNumber"]);
            Assert.AreEqual(users.data.Count, 6);
        }

        [Given(@"I request the info of user id (.*)")]
        public void GivenIRequestTheInfoOfUserId(int userId)
        {
            var request = new RestRequest("api/users/{id}");
            request.AddUrlSegment("id", userId);
            var response = client.Get(request);
            Assert.IsNotNull(response);
            _scenarioContext["statusCode"] = response.StatusCode.ToString();
            _scenarioContext["getUserResp"] = response.Content;
        }

        [Then(@"the API response contains the details of user id (.*)")]
        public void ThenTheAPIResponseContainsTheDetailsOfUserId(int userId)
        {
            Assert.AreEqual("OK", (string)_scenarioContext["statusCode"]);
            string response = (string)_scenarioContext["getUserResp"];
            Assert.IsNotNull(response);
            userJson u1 = JsonConvert.DeserializeObject<userJson>(response);
            Assert.AreEqual(u1.data.id, userId);
        }

        [Given(@"I submit the info of new user")]
        public void GivenISubmitTheInfoOfNewUser(Table table)
        {
            var request = new RestRequest("api/users");
            var person = table.CreateInstance<Person>();
            var pStr = JsonConvert.SerializeObject(person);
            request.AddBody(pStr, ContentType.Json);
            var response = client.Post(request);
            Assert.IsNotNull(response);
            Assert.AreEqual("Created", response.StatusCode.ToString());
            _scenarioContext["newUserResp"] = response.Content;
            _scenarioContext["newUserInput"] = person;
        }

        [Then(@"the API should return the new user's info")]
        public void ThenTheAPIShouldReturnTheNewUsersInfo()
        {
            string response = (string)_scenarioContext["newUserResp"];
            Assert.IsNotNull(response);
            Person output = JsonConvert.DeserializeObject<Person>(response);
            Person input = (Person)_scenarioContext["newUserInput"];
            Assert.AreEqual(input.name, output.name);
            Assert.AreEqual(input.job, output.job);
            Assert.IsTrue(output.id.Length > 0);
            Assert.IsTrue(output.createdAt.Length > 0);
        }

        [Given(@"I submit the request to update user")]
        public void GivenISubmitTheRequestToUpdateUser(Table table)
        {
            var request = new RestRequest("api/users/{id}");
            var person = table.CreateInstance<Person>();
            var pStr = JsonConvert.SerializeObject(person);
            request.AddBody(pStr, ContentType.Json);
            request.AddUrlSegment("id", person.id);
            var response = client.Put(request);
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.StatusCode.ToString());
            _scenarioContext["putUserResp"] = response.Content;
            _scenarioContext["input"] = person;
        }

        [Then(@"the API should return the updated user's info")]
        public void ThenTheAPIShouldReturnTheUpdatedUsersInfo()
        {
            string response = (string)_scenarioContext["putUserResp"];
            Assert.IsNotNull(response);
            Person output = JsonConvert.DeserializeObject<Person>(response);
            Person input = (Person)_scenarioContext["input"];
            Assert.AreEqual(input.name, output.name);
            Assert.AreEqual(input.job, output.job);
            Assert.IsTrue(output.updatedAt.Length > 0);
        }

        [Given(@"I submit the request to update user's specific info")]
        public void GivenISubmitTheRequestToUpdateUsersSpecificInfo(Table table)
        {
            var request = new RestRequest("api/users/{id}");
            var person = table.CreateInstance<Person>();
            var pStr = JsonConvert.SerializeObject(person);
            request.AddBody(pStr, ContentType.Json);
            request.AddUrlSegment("id", person.id);
            var response = client.Patch(request);
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.StatusCode.ToString());
            _scenarioContext["putUserResp"] = response.Content;
            _scenarioContext["input"] = person;
        }

        [Then(@"a (.*) Not Found is retured")]
        public void ThenANotFoundIsRetured(int p0)
        {
            Assert.AreEqual("NotFound", (string)_scenarioContext["statusCode"]);
        }

        [Then(@"verify that user (.*) is found in the list")]
        public void ThenVerifyThatUserIsFoundInTheList(string name)
        {
            string response = (string)_scenarioContext["getUsersResp"];
            Users users = JsonConvert.DeserializeObject<Users>(response);
            var user = users.data.Where(user => user.first_name.Equals(name.Split(" ")[0]) && user.last_name.Equals(name.Split(" ")[1]));
            Assert.IsNotEmpty(user);
        }

        [Given(@"I call the Login API with valid credentials")]
        public void GivenICallTheLoginAPIWithValidCredentials(Table table)
        {
            var credo = table.CreateInstance<loginCredo>();
            var valueBytes = System.Convert.FromBase64String(credo.password);
            var pwd = System.Text.Encoding.UTF8.GetString(valueBytes);
            credo.password = pwd;
            var json = JsonConvert.SerializeObject(credo);
            var request = new RestRequest("api/login");
            request.AddBody(json, ContentType.Json);
            var response = client.Post(request);
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.StatusCode.ToString());
            _scenarioContext["output"] = response.Content;
        }

        [Then(@"the API responds with a valid token")]
        public void ThenTheAPIRespondsWithAValidToken()
        {
            string response = (string)_scenarioContext["output"];
            loginCredo output = JsonConvert.DeserializeObject<loginCredo>(response);
            Assert.IsTrue(output.token.Length > 0);
        }

    }
}
