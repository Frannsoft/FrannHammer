using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Core.Models;

namespace KuroganeHammer.Data.Core.DTOs
{
    public class CharacterAttributeDto : BaseModel
    {
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public SmashAttributeTypeDto SmashAttributeTypeDto { get; set; }
        public int SmashAttributeTypeId { get; set; }

        public override async Task<HttpResponseMessage> Create(HttpClient client)
        {
            var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/characterattributes/{Id}", this);
            return httpResponseMessage;
        }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            return client.PutAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/characterattributes/{Id}", this);
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            return client.DeleteAsync($"{client.BaseAddress.AbsoluteUri}/characterattributes/{Id}");
        }
    }


}