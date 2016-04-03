using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Smoke
{
    public class BaseSmokeTest
    {
        protected HttpClient LoggedInAdminClient;
        protected HttpClient LoggedInBasicClient;
        protected HttpClient AnonymousClient;
        protected string Baseuri { get; private set; }
        private string _adminauthToken;
        private string _basicauthToken;
        protected const string CharactersRoute = "characters";
        protected const string MovementsRoute = "movements";
        protected const string MovesRoute = "moves";
        protected const string SmashAttributeTypeRoute = "smashattributetypes";
        protected const string CharacterAttributeRoute = "characterattributes";

        [SetUp]
        public virtual void SetUp()
        {
            Baseuri = ConfigurationManager.AppSettings["site"];
            var tokenUri = Baseuri + "/Token";
            LoggedInAdminClient = new HttpClient { BaseAddress = new Uri(Baseuri) };
            var admincontent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", ConfigurationManager.AppSettings["adminusername"]),
                new KeyValuePair<string, string>("password", ConfigurationManager.AppSettings["adminpassword"])
            });

            LoggedInBasicClient = new HttpClient { BaseAddress = new Uri(Baseuri) };
            var basiccontent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", ConfigurationManager.AppSettings["basicusername"]),
                new KeyValuePair<string, string>("password", ConfigurationManager.AppSettings["basicpassword"])
            });

            var adminresponse = LoggedInAdminClient.PostAsync(tokenUri, admincontent).Result;
            _adminauthToken =
                JsonConvert.DeserializeObject<dynamic>(adminresponse.Content.ReadAsStringAsync().Result).access_token;
            LoggedInAdminClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminauthToken);

            var basicresponse = LoggedInBasicClient.PostAsync(tokenUri, basiccontent).Result;
            _basicauthToken = JsonConvert.DeserializeObject<dynamic>(basicresponse.Content.ReadAsStringAsync().Result).access_token;
            LoggedInBasicClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _basicauthToken);

            AnonymousClient = new HttpClient { BaseAddress = new Uri(Baseuri) };
        }
    }
}
