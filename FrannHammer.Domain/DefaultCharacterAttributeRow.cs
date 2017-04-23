using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using System.Linq;
using FrannHammer.Utility;

namespace FrannHammer.Domain
{
    public class DefaultCharacterAttributeRow : ICharacterAttributeRow
    {
        [FriendlyName(FriendlyNameCommonConstants.Id)]
        public int Id { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }

        [FriendlyName("characterName")]
        public string CharacterName { get; }

        [FriendlyName("values")]
        public IEnumerable<IAttribute> Values { get; }

        public DefaultCharacterAttributeRow(IList<IAttribute> attributes)
        {
            Guard.VerifyObjectNotNull(attributes, nameof(attributes));
            Values = attributes;

            CharacterName = attributes.SingleOrDefault(a => a.Name.Equals("character", StringComparison.OrdinalIgnoreCase))?.Value;
        }

    }
}
