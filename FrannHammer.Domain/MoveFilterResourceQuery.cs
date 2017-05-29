using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class MoveFilterResourceQuery : IMoveFilterResourceQuery
    {
        [FriendlyName(FriendlyNameMoveCommonConstants.MoveTypeName)]
        public string MoveType { get; set; }

        [FriendlyName(FriendlyNameMoveCommonConstants.OwnerName)]
        public string Name { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string MoveName { get; set; }

        public string Fields { get; set; }
    }
}
