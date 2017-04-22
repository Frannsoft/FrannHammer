using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Movement : IMovement
    {
        [FriendlyName(FriendlyNameCommonConstants.Id)]
        public int Id { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }

        [FriendlyName("ownerId")]
        public int OwnerId { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }
    }
}
