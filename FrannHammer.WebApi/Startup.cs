using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FrannHammer.DefaultContainer.Configuration.ContainerModules;
using FrannHammer.Domain;
using FrannHammer.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.ActionFilterAttributes;
using FrannHammer.WebApi.HypermediaServices;
using FrannHammer.WebApi.Models;
using FrannHammer.WebApi.SwaggerExtensions;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(Startup))]

namespace FrannHammer.WebApi
{
    public class Startup
    {
        internal static IContainer Container { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

#if DEBUG
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#elif !DEBUG
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
#endif

            //configure container
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DatabaseModule>();
            containerBuilder.RegisterModule<RepositoryModule>();
            containerBuilder.RegisterModule<ApiServicesModule>();
            containerBuilder.RegisterModule<ModelModule>();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterType<AutofacContractResolver>()
                .AsSelf()
                .WithParameter((pi, c) => pi.Name == "container",
                    (pi, c) => Container);

            //automapper config - must be called prior to registering it in autofac
            //this way the static Mapper is initialized.
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ICharacter, CharacterResource>();
                cfg.CreateMap<IMove, MoveResource>();
            });

            containerBuilder.RegisterType<EntityToBusinessTranslationService>().As<IEntityToBusinessTranslationService>();
            containerBuilder.RegisterType<CharacterResourceEnricher>().AsSelf();
            containerBuilder.RegisterType<ManyMoveResourceEnricher>().AsSelf();


            containerBuilder.RegisterInstance(Mapper.Instance).ExternallyOwned();
            containerBuilder.RegisterWebApiFilterProvider(config);
            containerBuilder.RegisterType<LinkProvider>().As<ILinkProvider>();
            containerBuilder.RegisterType<SingleCharacterResourceHalSupportAttribute>().PropertiesAutowired();

            Container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            config.MessageHandlers.Add(new EnrichingHandler());
            config.AddResponseEnrichers(
                Container.Resolve<CharacterResourceEnricher>(),
                Container.Resolve<ManyMoveResourceEnricher>());

            //configure mongo db model mapping
            var mongoDbBsonMapper = new MongoDbBsonMapper();

            var assembliesToScanForModels = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(a => a.FullName.StartsWith("FrannHammer"))
                .Select(Assembly.Load).ToList();

            BsonMapper.RegisterTypeWithAutoMap<MongoModel>();
            mongoDbBsonMapper.MapAllLoadedTypesDerivingFrom<MongoModel>(assembliesToScanForModels);

            //character attribute is special.. thanks to mongodb not being able to deserialize to interfaces naturally (FINE.)
            BsonMapper.RegisterClassMaps(typeof(CharacterAttribute));

            //Register IMove implementation to move implementation map
            MoveParseClassMap.RegisterType<IMove, Move>();

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v0.5.0", "FrannHammer Api")
                        .Contact(cc => cc.Email("FrannSoftDev@outlook.com")
                                    .Name("@FrannDotExe")
                                    .Url("https://github.com/Frannsoft/FrannHammer/wiki"))
                    .Description("Restful api for Sm4sh frame data as told by @KuroganeHammer.")
                    .License(lc => lc.Name("License: MIT").Url("https://github.com/Frannsoft/FrannHammer/blob/develop/License.md"));
                c.DocumentFilter(() => new SwaggerAccessDocumentFilter());
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\XmlDocument.XML");
            })
               .EnableSwaggerUi(c =>
               {
                   c.InjectStylesheet(Assembly.GetExecutingAssembly(),
                       "FrannHammer.WebApi.SwaggerExtensions.swagger.styles.css");
               });

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new AutofacContractResolver(Container);

            app.UseAutofacMiddleware(Container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}