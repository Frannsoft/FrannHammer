using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class MoveFilterResourceQuery : IMoveFilterResourceQuery
    {
        [FriendlyName(FriendlyNameMoveCommonConstants.MoveTypeName)]
        public string MoveType { get; set; }

        [FriendlyName(FriendlyNameMoveCommonConstants.OwnerName)]
        public string CharacterName { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }
    }
}
