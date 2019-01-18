using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Movements;

namespace FrannHammer.WebScraping.Movements
{
    public class DefaultMovementProvider : IMovementProvider
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;

        public DefaultMovementProvider(IInstanceIdGenerator instanceIdGenerator)
        {
            _instanceIdGenerator = instanceIdGenerator;
        }

        public IMovement Create() => new Movement() { InstanceId = _instanceIdGenerator.GenerateId() };
    }
}