using FrannHammer.Domain.Contracts;
using static FrannHammer.Domain.FriendlyNameMoveCommonConstants;
using static FrannHammer.Domain.FriendlyNameCommonConstants;

namespace FrannHammer.Domain
{
    public class Movement : MongoModel, IMovement
    {
        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }

        [FriendlyName(OwnerName)]
        public string Owner { get; set; }
    }
}
