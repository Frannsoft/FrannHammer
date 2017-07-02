using FrannHammer.Domain.Contracts;
using static FrannHammer.Domain.FriendlyNameCommonConstants;
using static FrannHammer.Domain.FriendlyNameMoveCommonConstants;

namespace FrannHammer.Domain
{
    public class CharacterAttribute : IAttribute
    {
        [FriendlyName("value")]
        public string Value { get; set; }

        [FriendlyName(OwnerName)]
        public string Owner { get; set; }

        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }

    }
}
