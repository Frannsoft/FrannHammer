using System;
using Autofac;
using Newtonsoft.Json.Serialization;

namespace FrannHammer.WebApi
{
    public class AutofacContractResolver : DefaultContractResolver
    {
        private readonly IContainer _container;

        public AutofacContractResolver(IContainer container)
        {
            _container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract contract = base.CreateObjectContract(objectType);

            // use Autofac to create types that have been registered with it
            if (_container.IsRegistered(objectType))
            {
                contract.DefaultCreator = () => _container.Resolve(objectType);
            }

            return contract;
        }
    }
}