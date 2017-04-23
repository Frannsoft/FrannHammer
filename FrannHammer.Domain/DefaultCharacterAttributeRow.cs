using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using System.Linq;
using FrannHammer.Utility;

namespace FrannHammer.Domain
{
    public class DefaultCharacterAttributeRow : ICharacterAttributeRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CharacterName { get; }
        public IEnumerable<IAttribute> Values { get; }

        public DefaultCharacterAttributeRow(IList<IAttribute> attributes)
        {
            Guard.VerifyObjectNotNull(attributes, nameof(attributes));
            Values = attributes;

            CharacterName = attributes.Single(a => a.Name.Equals("character", StringComparison.OrdinalIgnoreCase)).Value;
        }

    }
}
