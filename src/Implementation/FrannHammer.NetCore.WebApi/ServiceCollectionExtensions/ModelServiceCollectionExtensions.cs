using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class ModelServiceCollectionExtensions
    {
        public static IServiceCollection AddModelSupport(this IServiceCollection services)
        {
            services.AddTransient<IAttribute, CharacterAttribute>();
            services.AddTransient<ICharacterAttributeName, CharacterAttributeName>();
            services.AddTransient<ICharacterAttributeRow, CharacterAttributeRow>();
            services.AddTransient<ICharacter, Character>();
            services.AddTransient<IMove, Move>();
            services.AddTransient<IMovement, Movement>();
            services.AddTransient<ICharacterDetailsDto, CharacterDetailsDto>();
            services.AddTransient<IUniqueData, UniqueData>();

            return services;
        }
    }
}
