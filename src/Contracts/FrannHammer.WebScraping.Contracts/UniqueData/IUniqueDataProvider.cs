
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.UniqueData
{
    public interface IUniqueDataProvider
    {
        T Create<T>() where T : class, IUniqueData, new();
    }
}
