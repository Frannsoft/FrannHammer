using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Core;
using FrannHammer.Core.Requests;

namespace FrannHammer.DataSynchro.Models
{
    public class UserModel
    {
        internal readonly HttpClient LoggedInClient;
        public string BaseUrl => LoggedInClient.BaseAddress.AbsoluteUri;
        public string Username { get; }

        private UserModel(HttpClient loggedInClient, string username)
        {
            Guard.VerifyObjectNotNull(loggedInClient, nameof(loggedInClient));

            LoggedInClient = loggedInClient;
            Username = username;
        }

        public static async Task<UserModel> LoginAs(string username, string password, string baseUrl)
        {
            var client = await AccountRequest.LoginAs(username, password, baseUrl);

            return new UserModel(client, username);
        }

        /// <summary>
        /// Returns true if user was logged out, false if failed to logout.
        /// </summary>
        /// <returns></returns>
        public void Logout() => new AccountRequest(LoggedInClient).Logout();
    }
}
