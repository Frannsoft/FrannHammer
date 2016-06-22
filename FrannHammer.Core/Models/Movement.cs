using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Core.Models
{
    public class Movement : BaseModel
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string Value { get; set; }

        public override async Task<HttpResponseMessage> Create(HttpClient client)
        {
            var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/movements", this);
            return httpResponseMessage;
        }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            return client.PutAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/movements/{Id}", this);
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            return client.DeleteAsync($"{client.BaseAddress.AbsoluteUri}/movements");
        }
    }
}