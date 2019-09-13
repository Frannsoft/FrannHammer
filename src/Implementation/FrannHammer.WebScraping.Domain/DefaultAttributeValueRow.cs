using System.Collections.Generic;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class DefaultAttributeValueRow : IAttributeValueRow
    {
        public IEnumerable<IAttributeValue> Values { get; set; }

        public DefaultAttributeValueRow(IEnumerable<IAttributeValue> values)
        {
            Guard.VerifyObjectNotNull(values, nameof(values));
            Values = values;
        }
    }
}
