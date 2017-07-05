using FrannHammer.Domain.Contracts;
using static FrannHammer.Domain.FriendlyNameCommonConstants;
using static FrannHammer.Domain.FriendlyNameMoveCommonConstants;

namespace FrannHammer.Domain
{
    public class UniqueData : MongoModel, IUniqueData
    {
        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }

        [FriendlyName(OwnerName)]
        public string Owner { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }
    }
}
