using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Core.DTOs;
using FrannHammer.Core.Models;

namespace FrannHammer.Core.Requests
{
    /// <summary>
    /// Assists with creating Character API methods.
    /// </summary>
    public class CharacterRequest : Request
    {
        public CharacterRequest(HttpClient client)
            : base(client)
        { }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            var response = await ExecuteAsync(async () => await Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/characters"));

            var characters = await response.Content.ReadAsAsync<List<Character>>();

            return characters;
        }

        public async Task<Character> GetCharacter(int id)
        {
            var response = await ExecuteAsync(async () => await Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/characters/" + id));

            var character = await response.Content.ReadAsAsync<Character>();

            return character;
        }

        public async Task<IEnumerable<CharacterAttributeDto>> GetCharacterAttributesOfCharacter(int id)
        {
            var response = await ExecuteAsync(async () =>
                await Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characters/{id}/characterattributes"));

            var attributes = await response.Content.ReadAsAsync<List<CharacterAttributeDto>>();

            return attributes;
        }

    }
}
