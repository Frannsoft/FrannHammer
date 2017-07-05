using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.UniqueData;

namespace FrannHammer.WebScraping.Unique
{
    public class DefaultUniqueDataProvider : IUniqueDataProvider
    {
        public IUniqueData Create()
        {
            return new UniqueData();
        }
    }
}
