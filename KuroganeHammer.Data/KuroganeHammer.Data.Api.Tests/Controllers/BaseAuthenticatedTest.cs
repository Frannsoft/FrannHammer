using Microsoft.Owin.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    public class BaseAuthenticatedTest : BaseControllerTest
    {
        protected virtual string Username => "GETuser";
        protected virtual string Password => "GETpassword";

        private string _token;

        protected override void PostSetup(TestServer server)
        {
            var tokenDetails = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", Username),
                new KeyValuePair<string, string>("password", Password)
            });

            var tokenResult = server.HttpClient.PostAsync("/api/token", tokenDetails).Result;
            Assert.That(tokenResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            _token = (string)body["access_token"];
        }

        protected override async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await _server.CreateRequest(uri)
                .AddHeader("Authorization", "Bearer" + _token)
                .GetAsync();
        }
    }
}
