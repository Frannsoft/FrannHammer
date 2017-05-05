using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FrannHammer.DefaultContainer.Configuration.ContainerModules;
using FrannHammer.Domain;
using FrannHammer.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using MongoDB.Bson.Serialization;
using Owin;
using System.Linq;

[assembly: OwinStartup(typeof(Startup))]

namespace FrannHammer.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            //configure container
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DatabaseModule>();
            containerBuilder.RegisterModule<RepositoryModule>();
            containerBuilder.RegisterModule<ApiServicesModule>();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //configure mongo db model mapping
            var mongoDbBsonMapper = new MongoDbBsonMapper();
            mongoDbBsonMapper.MapAllLoadedTypesDerivingFrom<MongoModel>(
                Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(a => a.FullName.StartsWith("FrannHammer"))
                .Select(Assembly.Load));

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}