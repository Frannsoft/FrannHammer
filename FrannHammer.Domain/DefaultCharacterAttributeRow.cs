using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class DefaultCharacterAttributeRow : MongoModel, ICharacterAttributeRow
    {
        [FriendlyName("characterName")]
        public string CharacterName { get; set; }

        [FriendlyName("values")]
        public IEnumerable<IAttribute> Values { get; set; }
    }
}
