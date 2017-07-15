using Autofac;
using FrannHammer.WebApi.HypermediaServices;

namespace FrannHammer.WebApi
{
    public class ResourceEnrichmentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EntityToBusinessTranslationService>().As<IEntityToBusinessTranslationService>();
            builder.RegisterType<CharacterResourceEnricher>().AsSelf();
            builder.RegisterType<ManyCharacterResourceEnricher>().AsSelf();
            builder.RegisterType<MoveResourceEnricher>().AsSelf();
            builder.RegisterType<ManyMoveResourceEnricher>().AsSelf();
            builder.RegisterType<MovementResourceEnricher>().AsSelf();
            builder.RegisterType<ManyMovementResourceEnricher>().AsSelf();
            builder.RegisterType<CharacterAttributeRowResourceEnricher>().AsSelf();
            builder.RegisterType<ManyCharacterAttributeRowResourceEnricher>().AsSelf();
            builder.RegisterType<CharacterAttributeNameResourceEnricher>().AsSelf();
            builder.RegisterType<ManyCharacterAttributeNameResourceEnricher>().AsSelf();
            builder.RegisterType<UniqueDataResourceEnricher>().AsSelf();
            builder.RegisterType<ManyUniqueDataResourceEnricher>().AsSelf();
            builder.RegisterType<LinkProvider>().As<ILinkProvider>();
        }
    }
}