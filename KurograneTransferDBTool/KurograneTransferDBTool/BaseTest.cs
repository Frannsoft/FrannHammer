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
        protected HttpClient LoggedInAdminClient;
        protected HttpClient LoggedInBasicClient;
        protected HttpClient AnonymousClient;
        protected const string Baseuri = "http://localhost:53410/api/";
        private string _adminauthToken;
        private string _basicauthToken;

        [SetUp]
        public void SetUp()
        {
            const string tokenUri = "http://localhost:53410/oauth/token";
            LoggedInAdminClient = new HttpClient {BaseAddress = new Uri(Baseuri)};
            var admincontent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "KuroUser"),
                new KeyValuePair<string, string>("password", "CrazyHand71!")
            });

            LoggedInBasicClient = new HttpClient { BaseAddress = new Uri(Baseuri) };
            var basiccontent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "GETuser"),
                new KeyValuePair<string, string>("password", "GETpassword")
            });

            var adminresponse = LoggedInAdminClient.PostAsync(tokenUri, admincontent).Result;
            _adminauthToken =
                JsonConvert.DeserializeObject<dynamic>(adminresponse.Content.ReadAsStringAsync().Result).access_token;
            LoggedInAdminClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminauthToken);

            var basicresponse = LoggedInBasicClient.PostAsync(tokenUri, basiccontent).Result;
            _basicauthToken = JsonConvert.DeserializeObject<dynamic>(basicresponse.Content.ReadAsStringAsync().Result).access_token;
            LoggedInBasicClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _basicauthToken);

            AnonymousClient = new HttpClient {BaseAddress = new Uri(Baseuri)};
        }
    }
}
