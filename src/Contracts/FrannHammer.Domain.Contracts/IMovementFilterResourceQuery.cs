namespace FrannHammer.Domain.Contracts
{
    public interface IMovementFilterResourceQuery : IFilterResourceQuery
    {
        string MovementName { get; set; }
        string Fields { get; set; }
    }
}
