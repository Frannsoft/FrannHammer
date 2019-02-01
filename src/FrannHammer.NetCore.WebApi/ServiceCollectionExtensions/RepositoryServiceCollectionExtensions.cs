using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositorySupport(this IServiceCollection services)
        {
            services.AddTransient<IRepository<ICharacter>, InMemoryRepository<ICharacter>>();
            services.AddTransient<IRepository<IMove>, InMemoryRepository<IMove>>();
            services.AddTransient<IRepository<IMovement>, InMemoryRepository<IMovement>>();
            services.AddTransient<IRepository<ICharacterAttributeRow>, InMemoryRepository<ICharacterAttributeRow>>();
            services.AddTransient<IRepository<IUniqueData>, InMemoryRepository<IUniqueData>>();

            return services
        }
    }
}
