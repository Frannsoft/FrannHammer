using System.Collections.Generic;
using Autofac;
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
using FrannHammer.WebScraping.Contracts.WebClients;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class ScrapingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultHtmlParserProvider>().As<IHtmlParserProvider>();
            builder.RegisterType<DefaultMovementProvider>().As<IMovementProvider>();
            builder.RegisterType<DefaultMoveProvider>().As<IMoveProvider>();
            builder.RegisterType<DefaultPageDownloader>().As<IPageDownloader>();
            builder.RegisterType<DefaultWebClientProvider>().As<IWebClientProvider>();
            builder.RegisterType<DefaultAttributeProvider>().As<IAttributeProvider>();
            builder.RegisterType<DefaultImageScrapingProvider>().As<IImageScrapingProvider>();
            builder.RegisterType<DefaultImageScrapingService>().As<IImageScrapingService>();
            builder.RegisterType<DefaultWebServices>().As<IWebServices>();
            builder.RegisterType<DefaultAttributeScrapingServices>().As<IAttributeScrapingServices>();
            builder.RegisterType<DefaultMoveScrapingServices>().As<IMoveScrapingServices>();
            builder.RegisterType<DefaultMovementScrapingServices>().As<IMovementScrapingServices>();

            builder.RegisterType<GroundMoveScraper>().AsSelf();
            builder.RegisterType<AerialMoveScraper>().AsSelf();
            builder.RegisterType<SpecialMoveScraper>().AsSelf();
            builder.RegisterType<ThrowMoveScraper>().AsSelf();

            builder.RegisterType<DefaultCharacterMoveScraper>()
                .As<ICharacterMoveScraper>()
                .WithParameter((pi, c) => pi.Name == "scrapers",
                    (pi, c) => new List<IMoveScraper>
                    {
                         c.Resolve<GroundMoveScraper>(),
                         c.Resolve<AerialMoveScraper>(),
                         c.Resolve<SpecialMoveScraper>(),
                         c.Resolve<ThrowMoveScraper>()
                    });

            builder.RegisterType<DefaultMovementScraper>().As<IMovementScraper>();

            builder.RegisterType<DefaultCharacterDataScrapingServices>()
                .As<ICharacterDataScrapingServices>()
                .WithParameter((pi, c) => pi.Name == "attributeScrapers",
                    (pi, c) =>
                    {
                        var attributeScrapingServices = c.Resolve<IAttributeScrapingServices>();
                        return AttributeScrapers.AllWithScrapingServices(attributeScrapingServices);
                    });

            builder.RegisterType<DefaultCharacterDataScraper>().As<ICharacterDataScraper>();
        }
    }
}
