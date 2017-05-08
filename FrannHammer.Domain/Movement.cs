using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Movement : MongoModel, IMovement
    {
        [FriendlyName("ownerId")]
        public int OwnerId { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }
    }
}
