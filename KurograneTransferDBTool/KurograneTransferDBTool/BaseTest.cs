using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KurograneTransferDBTool
{
    public class BaseTest
    {
        protected HttpClient LoggedInClient;
        protected HttpClient AnonymousClient;
        protected const string Baseuri = "http://localhost/frannhammerAPI/";
        private string _authToken;

        [SetUp]
        public void SetUp()
        {
            LoggedInClient = new HttpClient {BaseAddress = new Uri(Baseuri)};
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "KuroUser"),
                new KeyValuePair<string, string>("password", "")
            });

            var response = LoggedInClient.PostAsync("oauth/token", content).Result;
            _authToken =
                JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result).access_token;

            LoggedInClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);

            AnonymousClient = new HttpClient {BaseAddress = new Uri(Baseuri)};
        }
    }
}
