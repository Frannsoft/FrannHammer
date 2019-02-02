using AutoMapper;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices.MapServices;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.WebScraping.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperSupport(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<WebCharacter, Character>();
                cfg.CreateMap<ICharacter, CharacterResource>();
                cfg.CreateMap<IMove, MoveResource>();
                cfg.CreateMap<IMove, ExpandedMoveResource>()
                .ForMember(
                    dest => dest.BaseDamage,
                    opt => opt.MapFrom(src => new UltimateBaseDamageResourceMapService().MapFrom(src)))
                .ForMember(
                    dest => dest.HitboxActive,
                    opt => opt.MapFrom(src => new UltimateHitboxResourceMapService().MapFrom(src)));

                cfg.CreateMap<IMovement, MovementResource>();
                cfg.CreateMap<ICharacterAttributeRow, CharacterAttributeRowResource>();
                cfg.CreateMap<ICharacterAttributeName, CharacterAttributeNameResource>();
                cfg.CreateMap<IUniqueData, UniqueDataResource>();
            });

            services.AddSingleton(Mapper.Instance);

            return services;
        }
    }
}
