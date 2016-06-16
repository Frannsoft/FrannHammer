using System.Net.Http;

namespace KuroganeHammer.Data.Core.Requests
{
    public abstract class Request
    {
        protected HttpClient Client { get; }

        protected Request(HttpClient client)
        {
            Client = client;
        }

        protected Request()
        { }

    }
}
