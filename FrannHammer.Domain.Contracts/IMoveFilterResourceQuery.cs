namespace FrannHammer.Domain.Contracts
{
    public interface IMoveFilterResourceQuery
    {
        string Name { get; set; }
        string MoveName { get; set; }
        string MoveType { get; set; }
    }
}