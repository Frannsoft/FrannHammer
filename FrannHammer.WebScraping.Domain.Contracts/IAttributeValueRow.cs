using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public interface IAttributeValueRow
    {
        IEnumerable<IAttributeValue> Values { get; set; }
    }
}