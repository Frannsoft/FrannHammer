using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.UniqueData
{
    public interface IUniqueDataProvider
    {
        IUniqueData Create();
    }
}
