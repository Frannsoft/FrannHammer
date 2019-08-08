using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;

namespace FrannHammer.WebScraping.Moves
{
    public class DefaultMoveProvider : IMoveProvider
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;

        public DefaultMoveProvider(IInstanceIdGenerator instanceIdGenerator)
        {
            _instanceIdGenerator = instanceIdGenerator;
        }

        public IMove Create() => new Move() { InstanceId = _instanceIdGenerator.GenerateId() };
    }
}
