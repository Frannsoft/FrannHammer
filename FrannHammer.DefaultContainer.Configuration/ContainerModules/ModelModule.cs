using Autofac;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class ModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CharacterAttribute>().As<IAttribute>();
            builder.RegisterType<ICharacterAttributeRow>().AsImplementedInterfaces();
            builder.RegisterType<ICharacter>().AsImplementedInterfaces();
            builder.RegisterType<IMove>().AsImplementedInterfaces();
            builder.RegisterType<IMovement>().AsImplementedInterfaces();
        }
    }
}
