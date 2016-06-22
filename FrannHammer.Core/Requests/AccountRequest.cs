using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrannHammer.Core.Requests
{
    /// <summary>
    /// Assists with creating calls to the Account API methods.
    /// </summary>
    public class AccountRequest : Request
    {
        public AccountRequest(HttpClient client)
            : base(client)
        { }

        public static async Task<HttpClient> LoginAs(string username, string password, string baseUrl)
        {
            Guard.VerifyStringIsNotNullOrEmpty(username, nameof(username));
            Guard.VerifyStringIsNotNullOrEmpty(password, nameof(password));

            var tokenUri = baseUrl + "/Token";
            var loggedInClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            var admincontent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            var adminResponse = await ExecuteAsync(async () => await loggedInClient.PostAsync(tokenUri, admincontent));

            var adminauthToken =
                JsonConvert.DeserializeObject<dynamic>(adminResponse.Content.ReadAsStringAsync().Result).access_token.Value;
            loggedInClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminauthToken);

            return loggedInClient;
        }

        public async Task Logout()
        {
            await
                ExecuteAsync(
                    async () =>
                        await
                            Client.PostAsync($"{Client.BaseAddress.AbsoluteUri}/Account/Logout",
                                new StringContent(string.Empty)));

        }


    }
}
