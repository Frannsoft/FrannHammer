using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Movements;

namespace FrannHammer.WebScraping.Movements
{
    public class MovementProvider : IMovementProvider
    {
        public IMovement Create() => new Movement();
    }
}
