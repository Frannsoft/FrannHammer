using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Core.Requests
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

        protected static async Task<HttpResponseMessage> ExecuteAsync(Func<Task<HttpResponseMessage>> operation)
        {
            var response = await operation();

#if DEBUG
            var json = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + json, ex); //add raw response
            }
#else
            response.EnsureSuccessStatusCode();
#endif
            return response;
        }

    }
}
