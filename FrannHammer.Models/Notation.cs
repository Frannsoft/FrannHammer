using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Models
{
    public class BaseNotationModel : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

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

    public class NotationDto : BaseNotationModel
    {
    }

    public class Notation : BaseNotationModel, IEntity
    {
        public DateTime LastModified { get; set; }
    }
}