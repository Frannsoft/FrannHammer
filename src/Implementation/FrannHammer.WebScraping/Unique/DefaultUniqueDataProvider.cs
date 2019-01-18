using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.UniqueData;

namespace FrannHammer.WebScraping.Unique
{
    public class DefaultUniqueDataProvider : IUniqueDataProvider
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;

        public DefaultUniqueDataProvider(IInstanceIdGenerator instanceIdGenerator)
        {
            _instanceIdGenerator = instanceIdGenerator;
        }

        public IUniqueData Create()
        {
            return new UniqueData() { InstanceId = _instanceIdGenerator.GenerateId() };
        }
    }
}
