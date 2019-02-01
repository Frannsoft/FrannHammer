using FrannHammer.WebScraping;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Character;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.Images;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Contracts.WebClients;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.Unique;
using FrannHammer.WebScraping.WebClients;
using Microsoft.Extensions.DependencyInjection;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class ScrapingServiceCollectionExtensions
    {
        public static IServiceCollection AddScrapingSupport(this IServiceCollection services)
        {
            services.AddTransient<IInstanceIdGenerator, InstanceIdGenerator>();
            services.AddTransient<IHtmlParserProvider, DefaultHtmlParserProvider>();
            services.AddTransient<IMovementProvider, DefaultMovementProvider>();
            services.AddTransient<IMoveProvider, DefaultMoveProvider>();
            services.AddTransient<IPageDownloader, DefaultPageDownloader>();
            services.AddTransient<IWebClientProvider, DefaultWebClientProvider>();
            services.AddTransient<IAttributeProvider, DefaultAttributeProvider>();
            services.AddTransient<IImageScrapingProvider, DefaultImageScrapingProvider>();
            services.AddTransient<IImageScrapingService, DefaultImageScrapingService>();
            services.AddTransient<IAttributeScrapingServices, DefaultAttributeScrapingServices>();
            services.AddTransient<IMoveScrapingServices, DefaultMoveScrapingServices>();
            services.AddTransient<IMovementScrapingServices, DefaultMovementScrapingServices>();
            services.AddTransient<IUniqueDataProvider, DefaultUniqueDataProvider>();
            services.AddTransient<GroundMoveScraper>();
            services.AddTransient<AerialMoveScraper>();
            services.AddTransient<SpecialMoveScraper>();
            services.AddTransient<ThrowMoveScraper>();

            services.AddTransient<ICharacterMoveScraper, DefaultCharacterMoveScraper>();
            services.AddTransient<IMovementScraper, DefaultMovementScraper>();
            services.AddTransient<ICharacterDataScrapingServices, DefaultCharacterDataScrapingServices>();
            services.AddTransient<ICharacterDataScraper, DefaultCharacterDataScraper>();
            services.AddTransient<IUniqueDataScrapingServices, DefaultUniqueDataScrapingServices>();

            services.AddSingleton<IWebServices, DefaultWebServices>();


            return services;
        }
    }
}
