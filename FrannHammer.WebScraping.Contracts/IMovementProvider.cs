using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IMovementProvider
    {
        IMovement Create();
    }
}
