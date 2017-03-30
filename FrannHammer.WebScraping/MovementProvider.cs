using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts;

namespace FrannHammer.WebScraping
{
    public class MovementProvider : IMovementProvider
    {
        public IMovement Create() => new Movement();
    }
}
