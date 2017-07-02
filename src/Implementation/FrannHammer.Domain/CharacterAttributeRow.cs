using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using static FrannHammer.Domain.FriendlyNameMoveCommonConstants;
using static FrannHammer.Domain.FriendlyNameCommonConstants;

namespace FrannHammer.Domain
{
    public class CharacterAttributeRow : MongoModel, ICharacterAttributeRow
    {
        [FriendlyName(OwnerName)]
        public string Owner { get; set; }

        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }

        [FriendlyName("values")]
        public IEnumerable<IAttribute> Values { get; set; }
    }
}
