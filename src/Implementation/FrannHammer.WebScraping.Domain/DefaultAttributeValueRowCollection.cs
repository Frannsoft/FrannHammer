using System.Collections.Generic;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class DefaultAttributeValueRowCollection : IAttributeValueRowCollection
    {
        public IEnumerable<IAttributeValueRow> AttributeValues { get; set; }

        public DefaultAttributeValueRowCollection(IEnumerable<IAttributeValueRow> values)
        {
            AttributeValues = values;
        }
    }
}
