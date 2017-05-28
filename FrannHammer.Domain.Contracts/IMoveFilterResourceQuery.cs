namespace FrannHammer.Domain.Contracts
{
    public interface IMoveFilterResourceQuery
    {
        string CharacterName { get; set; }
        string Name { get; set; }
        string MoveType { get; set; }
    }
}