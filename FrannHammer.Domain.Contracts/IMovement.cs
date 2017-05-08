namespace FrannHammer.Domain.Contracts
{
    public interface IMovement : IModel
    {
        int OwnerId { get; set; }
        string Value { get; set; }
    }
}
