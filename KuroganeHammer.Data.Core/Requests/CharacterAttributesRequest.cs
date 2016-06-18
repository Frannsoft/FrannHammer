using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Core.DTOs;

namespace KuroganeHammer.Data.Core.Requests
{
    public class CharacterAttributesRequest : Request
    {
        public CharacterAttributesRequest(HttpClient client)
            : base(client)
        { }

        public async Task<CharacterAttributeDto> GetCharacterAttribute(int id)
        {
            var response =
                await
                    ExecuteAsync(
                        async () => await Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characterattributes/{id}"));
            //var response = await Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/characterattributes/" + id);
            //response.EnsureSuccessStatusCode();

            var characterAttribute = await response.Content.ReadAsAsync<CharacterAttributeDto>();

            return characterAttribute;
        }


    }
}
