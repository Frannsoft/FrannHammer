using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public interface IUniqueDataProvider
    {
        IUniqueData Create(IDictionary<string, string> rawData);
    }
}
