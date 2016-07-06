using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Models
{
    public class Throw : BaseModel
    {
        public int Id { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public ThrowType ThrowType { get; set; }
        public int ThrowTypeId { get; set; }
        public bool WeightDependent { get; set; }
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
