﻿using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Autofac.Integration.WebApi;
using CacheCow.Server;
using CacheCow.Server.EntityTagStore.SqlServer;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using Swashbuckle.Application;
using WebApiThrottle;

namespace FrannHammer.Api
{
    public static class WebApiConfig
    {
        #region custom config keys
        private const string KeyThrottleRequestsPerSecond = "throttlingrequestspersecond";
        #endregion

        public static IConnectionMultiplexer RedisMultiplexer;

        static WebApiConfig()
        {
            RedisMultiplexer = ConnectionMultiplexer.Connect("localhost");
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container.Instance.AutoFacContainer);


#if !DEBUG
            int throttleRequestsPerSecond = GetConfigValue<int>(KeyThrottleRequestsPerSecond);

            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perSecond: throttleRequestsPerSecond)
                {
                    IpThrottling = true
                },
                Repository = new CacheRepository()
            });

            //cache on db in release mode
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var eTagStore = new SqlServerEntityTagStore(connectionString);

            var cacheCowCacheHandler = new CachingHandler(config, eTagStore)
            {
                AddLastModifiedHeader = false
            };
            config.MessageHandlers.Add(cacheCowCacheHandler);

#else
            //in memory caching for debug mode
            config.MessageHandlers.Add(new CachingHandler(config));
#endif
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "FrannHammer Api");
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\XmlDocument.XML");
            })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(typeof(WebApiConfig).Assembly,
                        "KuroganeHammer.Data.Api.SwaggerExtensions.onComplete.js");
                });

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static T GetConfigValue<T>(string key)
        {
            T retVal = default(T);
            var configValue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(configValue))
            { throw new Exception($"Unable to find key of {key}"); }

            retVal = (T)Convert.ChangeType(configValue, typeof(T));

            return retVal;
        }
    }
}
