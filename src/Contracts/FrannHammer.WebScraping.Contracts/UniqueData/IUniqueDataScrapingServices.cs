using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.UniqueData
{
    public interface IUniqueDataScrapingServices : IWebServices
    {
        IUniqueData Create();
    }
}
