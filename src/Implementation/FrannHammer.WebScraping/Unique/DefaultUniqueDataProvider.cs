using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.UniqueData;
using System;

namespace FrannHammer.WebScraping.Unique
{
    public class DefaultUniqueDataProvider : IUniqueDataProvider
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;

        public DefaultUniqueDataProvider(IInstanceIdGenerator instanceIdGenerator)
        {
            _instanceIdGenerator = instanceIdGenerator;
        }

        public T Create<T>() where T : class, IUniqueData, new()
        {
            var uniqueData = Activator.CreateInstance<T>();
            uniqueData.InstanceId = _instanceIdGenerator.GenerateId();
            return uniqueData;
        }
    }
}
