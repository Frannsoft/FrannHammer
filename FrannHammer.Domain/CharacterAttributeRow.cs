using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class CharacterAttributeRow : MongoModel, ICharacterAttributeRow
    {
        [FriendlyName("ownerId")]
        public string Owner { get; set; }

        [FriendlyName("values")]
        public IEnumerable<IAttribute> Values { get; set; }

        [FriendlyName("characterName")]
        public string CharacterName { get; set; }
    }
}
