using System.Net.Http;

namespace FrannHammer.WebApi.HypermediaServices
{
    public interface IResponseEnricher
    {
        bool CanEnrich(HttpResponseMessage response);
        HttpResponseMessage Enrich(HttpResponseMessage response);
    }
}