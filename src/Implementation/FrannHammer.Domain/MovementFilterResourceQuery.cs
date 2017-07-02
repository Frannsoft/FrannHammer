using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class MovementFilterResourceQuery : IMovementFilterResourceQuery
    {
        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string MovementName { get; set; }

        public string Fields { get; set; }
    }
}
