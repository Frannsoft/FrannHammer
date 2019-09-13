namespace FrannHammer.Domain.Contracts
{
    public interface IMoveFilterResourceQuery : IFilterResourceQuery
    {
        string Name { get; set; }
        string MoveName { get; set; }
        string MoveType { get; set; }
        int Id { get; set; }
    }
}