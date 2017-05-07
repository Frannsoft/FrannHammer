namespace FrannHammer.Domain.Contracts
{
    public interface IHaveAnId
    {
        string Id { get; set; }
    }

    public interface IHaveAName
    {
        string Name { get; set; }
    }

    public interface IModel : IHaveAnId, IHaveAName
    { }
}
