namespace FrannHammer.Domain.Contracts
{
    public interface IAttribute : IModel
    {
        int OwnerId { get; set; }
        string Owner { get; set; }
        string Value { get; set; }
        int SmashAttributeTypeId { get; set; }
        int CharacterAttributeTypeId { get; set; }
    }
}
