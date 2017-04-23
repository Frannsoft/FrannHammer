using FrannHammer.Domain.Contracts;
using MongoDB.Bson.Serialization.Attributes;

namespace FrannHammer.Domain
{
    [BsonDiscriminator(nameof(IAttribute))]
    public class CharacterAttribute : IAttribute
    {
        [FriendlyName(FriendlyNameCommonConstants.Id)]
        public int Id { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }

        [FriendlyName("ownerId")]
        public int OwnerId { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }

        [FriendlyName("smashAttributeTypeId")]
        public int SmashAttributeTypeId { get; set; }

        [FriendlyName("characterAttributeTypeId")]
        public int CharacterAttributeTypeId { get; set; }

        [FriendlyName("owner")]
        public string Owner { get; set; }
    }
}
