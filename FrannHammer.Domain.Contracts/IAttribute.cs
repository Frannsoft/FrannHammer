namespace FrannHammer.Domain.Contracts
{
    public interface IAttribute : IHaveAName
    {
        string Owner { get; set; }
        string Value { get; set; }
    }
}
