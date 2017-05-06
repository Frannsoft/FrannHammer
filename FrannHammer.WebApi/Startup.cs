﻿using System;
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
using FrannHammer.Domain.Contracts;
using Newtonsoft.Json.Serialization;

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

            Container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            //configure mongo db model mapping
            var mongoDbBsonMapper = new MongoDbBsonMapper();
            mongoDbBsonMapper.MapAllLoadedTypesDerivingFrom<MongoModel>(
                Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(a => a.FullName.StartsWith("FrannHammer"))
                .Select(Assembly.Load));

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


    public class AutofacContractResolver : DefaultContractResolver
    {
        private readonly IContainer _container;

        public AutofacContractResolver(IContainer container)
        {
            _container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract contract = base.CreateObjectContract(objectType);

            // use Autofac to create types that have been registered with it
            if (_container.IsRegistered(objectType))
            {
                contract.DefaultCreator = () => _container.Resolve(objectType);
            }

            return contract;
        }
    }
}