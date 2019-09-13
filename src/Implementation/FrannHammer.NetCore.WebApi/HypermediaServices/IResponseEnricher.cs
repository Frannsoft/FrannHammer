namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public interface IResponseEnricher
    {
        bool CanEnrich(object content);
        //HttpResponse Enrich(HttpResponse response);
        object Enrich(string existingContent);
    }
}
