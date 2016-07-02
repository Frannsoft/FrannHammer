using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Models
{
    public class CharacterAttributeType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Notation Notation { get; set; }
        public int? NotationId { get; set; }
        public DateTime LastModified { get; set; }

        public override async Task<HttpResponseMessage> Create(HttpClient client)
        {
            var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/characterattributes", this);
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