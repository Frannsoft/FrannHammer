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
        public override Task<HttpResponseMessage> Create(HttpClient client)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            throw new NotImplementedException();
        }
    }
}