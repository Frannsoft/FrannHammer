using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class CharacterAttribute : IAttribute
    {
        [FriendlyName("value")]
        public string Value { get; set; }

        [FriendlyName("owner")]
        public string Owner { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }
    }
}
