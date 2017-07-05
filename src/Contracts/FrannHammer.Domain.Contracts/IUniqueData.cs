namespace FrannHammer.Domain.Contracts
{
    public interface IUniqueData : IModel, IHaveAnOwner, IHaveAnOwnerId
    {
        string Value { get; set; }
    }
}
