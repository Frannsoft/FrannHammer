using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class CharacterAttribute : MongoModel, IAttribute
    {
        [FriendlyName("value")]
        public string Value { get; set; }

        [FriendlyName("owner")]
        public string Owner { get; set; }
    }
}
