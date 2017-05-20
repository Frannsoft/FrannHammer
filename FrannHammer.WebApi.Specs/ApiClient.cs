using System.Net.Http;
using Autofac;
using FrannHammer.Utility;
using Newtonsoft.Json;

namespace FrannHammer.WebApi.Specs
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            Guard.VerifyObjectNotNull(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }
        public HttpResponseMessage GetResult(string requestUri) => _httpClient.GetAsync(requestUri).Result;

        public T DeserializeResponse<T>(HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(responseMessage.Content.ReadAsStringAsync().Result,
                new JsonSerializerSettings
                {
                    ContractResolver = Startup.Container.Resolve<AutofacContractResolver>()
                });
        }
    }
}
