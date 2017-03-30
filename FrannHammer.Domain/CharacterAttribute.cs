using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class CharacterAttribute : IAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public int SmashAttributeTypeId { get; set; }
        public int CharacterAttributeTypeId { get; set; }
        public string AttributeFlag { get; set; }
    }
}
