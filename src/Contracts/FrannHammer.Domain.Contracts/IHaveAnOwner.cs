namespace FrannHammer.Domain.Contracts
{
    public interface IHaveAnOwner : IHaveAnOwnerId
    {
        string Owner { get; set; }
    }
}