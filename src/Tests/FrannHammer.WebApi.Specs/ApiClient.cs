using FrannHammer.Utility;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace FrannHammer.NetCore.WebApi.Specs
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
            string content = responseMessage.Content.ReadAsStringAsync().Result;

            try
            {
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e.Message}{Environment.NewLine} Reason Phrase:" +
                                               $" {responseMessage.ReasonPhrase}{Environment.NewLine}" +
                                               $"Request url: {responseMessage.RequestMessage}{Environment.NewLine} " +
                                               $"Content: {content}" +
                                               $"Raw exception: {e}");
            }
            return JsonConvert.DeserializeObject<T>(content);
            //new JsonSerializerSettings
            //{
            //    ContractResolver = Startup.Container.Resolve<AutofacContractResolver>()
            //});
        }
    }
}
