using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using FrannHammer.AccountRegistrationTool.Models;
using FrannHammer.Core;
using FrannHammer.Core.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.EntityFramework;
using FrannHammer.Api.DTOs;

namespace FrannHammer.AccountRegistrationTool
{
    public class ServiceHandler
    {
        private readonly HttpClient _client;
        private string _authToken;

        public ServiceHandler(string serviceUri)
        {
            _client = new HttpClient { BaseAddress = new Uri(serviceUri) };
        }

        public bool LoginAs(string username, string password)
        {
            Guard.VerifyStringIsNotNullOrEmpty(username, nameof(username));
            Guard.VerifyStringIsNotNullOrEmpty(password, nameof(password));

            var postDetails = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });
            var response = _client.PostAsync("token", postDetails).Result;

            _authToken = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result).access_token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);

            return !string.IsNullOrEmpty(_authToken);
        }

        public void RegisterNewUser(RegisterUserModel model)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));

            if (string.IsNullOrEmpty(_authToken))
            { throw new InvalidOperationException("You need to authenticate with LoginAs() first."); }

            //create user
            var response = CreateUser(model);
            if (!response.IsSuccessStatusCode)
            { throw new Exception(response.StatusCode + Environment.NewLine + response.ReasonPhrase);}

            var userId = response.Content.ReadAsAsync<UserDto>().Result.Id;

            //assign roles
            AssignRoles(userId, model.Roles);
        }

        private HttpResponseMessage CreateUser(RegisterUserModel model)
        {
            return _client.PostAsync("account/register", model.BindingModel, new JsonMediaTypeFormatter()).Result;
        }

        private void AssignRoles(string userId, Roles roles)
        {
            foreach (var role in roles.UserRoles)
            {
                //get roleid
                var roleId = _client.GetAsync("roles")
                    .Result
                    .Content
                    .ReadAsAsync<List<IdentityRole>>()
                    .Result
                    .First(r => r.Name.Equals(role))
                    .Id;

                var addUserToRoleModel = new UserToRoleModel
                {
                    RoleId = roleId,
                    UserId = userId
                };

                //add user to role
                var response =
                    _client.PostAsync("roles/addusertorole", addUserToRoleModel, new JsonMediaTypeFormatter()).Result;

                if (!response.IsSuccessStatusCode)
                { throw new Exception($"{response.StatusCode} - {response.ReasonPhrase}"); }
            }
        }
    }
}
