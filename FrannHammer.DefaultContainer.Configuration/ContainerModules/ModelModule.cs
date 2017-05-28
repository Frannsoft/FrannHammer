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
            builder.RegisterType<CharacterAttributeRow>().As<ICharacterAttributeRow>();
            builder.RegisterType<Character>().As<ICharacter>();
            builder.RegisterType<Move>().As<IMove>();
            builder.RegisterType<Movement>().As<IMovement>();
            builder.RegisterType<CharacterDetailsDto>().As<ICharacterDetailsDto>();
        }
    }
}
