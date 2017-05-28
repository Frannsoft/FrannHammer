using System;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
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
            JsonObjectContract contract = default(JsonObjectContract);//= base.CreateObjectContract(objectType);

            // use Autofac to create types that have been registered with it
            if (_container.IsRegistered(objectType))
            {
                contract = ResolveContact(objectType);
                contract.DefaultCreator = () => _container.Resolve(objectType);
            }
            else
            {
                contract = base.CreateObjectContract(objectType);
            }

            return contract;
        }

        private JsonObjectContract ResolveContact(Type objectType)
        {
            // attempt to create the contact from the resolved type
            IComponentRegistration registration;
            if (_container.ComponentRegistry.TryGetRegistration(new TypedService(objectType), out registration))
            {
                Type viewType = (registration.Activator as ReflectionActivator)?.LimitType;
                if (viewType != null)
                {
                    return base.CreateObjectContract(viewType);
                }
            }

            // fall back to using the registered type
            return base.CreateObjectContract(objectType);
        }
    }
}