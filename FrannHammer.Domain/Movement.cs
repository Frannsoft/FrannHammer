using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Movement : MongoModel, IMovement
    {
        [FriendlyName("ownerId")]
        public string Owner { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }
    }
}
