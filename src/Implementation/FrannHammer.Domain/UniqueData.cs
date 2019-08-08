using FrannHammer.Domain.Contracts;
using static FrannHammer.Domain.FriendlyNameCommonConstants;
using static FrannHammer.Domain.FriendlyNameMoveCommonConstants;

namespace FrannHammer.Domain
{
    public class UniqueData : BaseModel, IUniqueData
    {
        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }

        [FriendlyName(OwnerName)]
        public string Owner { get; set; }
    }
}
