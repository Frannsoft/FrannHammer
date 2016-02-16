using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace KurograneTransferDBTool
{
    public class BaseTest
    {
        protected HttpClient client;
        protected const string BASEURL = "http://localhost/frannhammerAPI/";
        string authToken;

        [SetUp]
        public void SetUp()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/frannHammerAPI");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "KuroUser"),
                new KeyValuePair<string, string>("password", "")
            });

            var response = client.PostAsync("/oauth/token", content).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            authToken = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result).access_token;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }
    }
}
