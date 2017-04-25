namespace FrannHammer.Domain.Contracts
{
    public interface IAttribute : IModel
    {
        string Owner { get; set; }
        string Value { get; set; }
    }
}
