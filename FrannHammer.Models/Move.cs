using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Models
{
    public class Move : BaseModel
    {
        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        public override async Task<HttpResponseMessage> Create(HttpClient client)
        {
            var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/moves", this);
            return httpResponseMessage;
        }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            return client.PutAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/moves/{Id}", this);
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            return client.DeleteAsync($"{client.BaseAddress.AbsoluteUri}/moves/{Id}");
        }
    }
}