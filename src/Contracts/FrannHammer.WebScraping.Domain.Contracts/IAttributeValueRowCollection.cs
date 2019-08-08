using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public interface IAttributeValueRowCollection
    {
        IEnumerable<IAttributeValueRow> AttributeValues { get; set; }
    }
}
