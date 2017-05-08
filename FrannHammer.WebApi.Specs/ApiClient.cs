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

        public T DeserializeResponse<T>(HttpContent responseContent)
        {
            return JsonConvert.DeserializeObject<T>(responseContent.ReadAsStringAsync().Result,
                new JsonSerializerSettings
                {
                    ContractResolver = Startup.Container.Resolve<AutofacContractResolver>()
                });
        }
    }
}
