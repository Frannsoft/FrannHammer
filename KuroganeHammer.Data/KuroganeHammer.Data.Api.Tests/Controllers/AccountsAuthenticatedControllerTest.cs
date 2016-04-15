using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    public class AccountsAuthenticatedControllerTest : BaseAuthenticatedTest
    {
        private string _uriBase = "/api/account";

        [Test]
        public async Task ShouldGetCharacters()
        {
            var uri = "/api/characters";
            var response = await GetAsync(uri);

            var characters = response.Content.ReadAsAsync<IEnumerable<Character>>()
                .Result
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.AllItemsAreUnique(characters);
        }
    }
}
