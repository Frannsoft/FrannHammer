using AutoMapper;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class EnrichmentServiceCollectionExtensions
    {
        public static IServiceCollection AddResourceEnrichers(this IServiceCollection services)
        {
            services.AddTransient<ILinkProvider, LinkProvider>();
            services.AddTransient<IEnrichmentProvider>(s =>
            {
                return new EnrichmentProvider(
                    s.GetService<ILinkProvider>(),
                    s.GetService<IMapper>(),
                    s.GetService<LinkGenerator>(),
                    s.GetService<IActionContextAccessor>().ActionContext.HttpContext);
            });

            return services;
        }
    }
}
