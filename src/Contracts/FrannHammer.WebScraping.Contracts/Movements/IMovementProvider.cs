using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Movements
{
    public interface IMovementProvider
    {
        IMovement Create();
    }
}
