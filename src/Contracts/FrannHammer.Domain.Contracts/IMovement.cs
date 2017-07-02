namespace FrannHammer.Domain.Contracts
{
    public interface IMovement : IModel, IHaveAnOwner
    {
        string Value { get; set; }
    }
}
