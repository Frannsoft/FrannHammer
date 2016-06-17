using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Core.Requests
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

            var adminresponse = await loggedInClient.PostAsync(tokenUri, admincontent);

#if DEBUG
            var json = adminresponse.Content.ReadAsStringAsync().Result;
            try
            {
                adminresponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + json, ex); //add raw response
            }
#else
            adminresponse.EnsureSuccessStatusCode();
#endif

            var adminauthToken =
                JsonConvert.DeserializeObject<dynamic>(adminresponse.Content.ReadAsStringAsync().Result).access_token.Value;
            loggedInClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminauthToken);

            return loggedInClient;
        }

        public async Task Logout()
        {
            await Client.PostAsync(Client.BaseAddress.AbsoluteUri + "/Account/Logout", new StringContent(string.Empty));
        }


    }
}
