using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class ApiServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<ICharacterAttributeNameProvider, DefaultCharacterAttributeNameProvider>();
            services.AddTransient<IAttributeStrategy, DefaultAttributeStrategy>();
            services.AddTransient<IQueryMappingService, QueryMappingService>();
            services.AddTransient<IDtoProvider, DefaultDtoProvider>();

            services.AddTransient<IGameParameterParserService, GameParameterParserService>(sp =>
            {
                sp.GetService<IActionContextAccessor>().ActionContext.HttpContext.Request.Query.TryGetValue("game", out StringValues game);
                return new GameParameterParserService(game.ToString());
            });

            services.AddTransient<ICharacterService, DefaultCharacterService>();
            services.AddTransient<IMoveService, DefaultMoveService>();
            services.AddTransient<IMovementService, DefaultMovementService>();
            services.AddTransient<IUniqueDataService, DefaultUniqueDataService>();
            services.AddTransient<ICharacterAttributeRowService, DefaultCharacterAttributeService>();

            return services;
        }
    }
}
