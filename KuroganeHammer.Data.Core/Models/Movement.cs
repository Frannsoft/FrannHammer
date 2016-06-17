using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KuroganeHammer.Data.Core.Models
{
    public class Movement : BaseModel
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string Value { get; set; }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            return client.PutAsJsonAsync(client.BaseAddress.AbsoluteUri + "/movements", this);
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            return client.DeleteAsync(client.BaseAddress.AbsoluteUri + "/movements");
        }
    }
}