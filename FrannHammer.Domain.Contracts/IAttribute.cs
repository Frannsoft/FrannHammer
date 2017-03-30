namespace FrannHammer.Domain.Contracts
{
    public interface IAttribute : IModel
    {
        int OwnerId { get; set; }
        string Rank { get; set; }
        string Value { get; set; }
        int SmashAttributeTypeId { get; set; }
        int CharacterAttributeTypeId { get; set; }
        string AttributeFlag { get; set; }
    }
}
