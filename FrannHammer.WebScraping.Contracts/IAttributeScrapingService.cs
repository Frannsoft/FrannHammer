using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IAttributeScrapingService
    {
        IEnumerable<IAttribute> GetAttributes();
    }
}
