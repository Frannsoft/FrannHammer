using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.UniqueData
{
    public interface IUniqueDataScrapingServices : IWebServices
    {
        T Create<T>() where T : class, IUniqueData, new();
    }
}
