using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class CharacterAttribute : IAttribute
    {
        [FriendlyName(FriendlyNameCommonConstants.Id)]
        public int Id { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }

        [FriendlyName("ownerId")]
        public int OwnerId { get; set; }

        [FriendlyName("rank")]
        public string Rank { get; set; }

        [FriendlyName("value")]
        public string Value { get; set; }

        [FriendlyName("smashAttributeTypeId")]
        public int SmashAttributeTypeId { get; set; }

        [FriendlyName("characterAttributeTypeId")]
        public int CharacterAttributeTypeId { get; set; }

        [FriendlyName("attributeFlag")]
        public string AttributeFlag { get; set; }
    }
}
