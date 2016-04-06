using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebApiThrottle;

namespace KuroganeHammer.Data.Api
{
    public static class WebApiConfig
    {
        #region custom config keys
        private const string KeyThrottleRequestsPerSecond = "throttlingrequestspersecond";
        #endregion

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
#endif

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
