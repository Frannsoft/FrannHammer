using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Models
{
    public abstract class BaseModel
    {
        public abstract Task<HttpResponseMessage> Create(HttpClient client);
        public abstract Task<HttpResponseMessage> Update(HttpClient client);
        public abstract Task<HttpResponseMessage> Delete(HttpClient client);
    }
}
