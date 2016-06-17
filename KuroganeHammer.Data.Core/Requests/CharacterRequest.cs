using System.Collections.Generic;
using System.Net.Http;
using KuroganeHammer.Data.Core.Models;

namespace KuroganeHammer.Data.Core.Requests
{
    /// <summary>
    /// Assists with creating Character API methods.
    /// </summary>
    public class CharacterRequest : Request
    {
        public CharacterRequest(HttpClient client) 
            : base(client)
        { }

        public IEnumerable<Character> GetCharacters()
        {
            var response = Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/characters").Result;

            response.EnsureSuccessStatusCode();

            var characters = response.Content.ReadAsAsync<List<Character>>().Result;

            return characters;
        }

        public Character GetCharacter(int id)
        {
            var response = Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/characters/" + id).Result;
            response.EnsureSuccessStatusCode();

            var character = response.Content.ReadAsAsync<Character>().Result;

            return character;
        }
        
    }
}
