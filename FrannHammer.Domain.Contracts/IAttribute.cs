namespace FrannHammer.Domain.Contracts
{
    public interface IAttribute : IHaveAName, IHaveAnOwner
    {
        string Value { get; set; }
    }
}
