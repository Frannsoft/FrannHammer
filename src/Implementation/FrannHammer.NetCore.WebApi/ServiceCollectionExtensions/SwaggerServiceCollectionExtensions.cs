using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace FrannHammer.NetCore.WebApi.ServiceCollectionExtensions
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerSupport(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v7", new Info
                {
                    Title = "The KuroganeHammer API",
                    Version = "v7",
                    Contact = new Contact
                    {
                        Name = "@FrannDotExe",
                        Url = "https://twitter.com/@franndotexe"
                    },
                    License = new License
                    {
                        Name = "License: MIT",
                        Url = "https://github.com/Frannsoft/FrannHammer/blob/develop/License.md"
                    },
                    Description = "Restful api for Smash4 and Ultimate frame data as told by @KuroganeHammer. To specify a game, " +
                    "add a query parameter of game=ultimate or game=smash4 to the end of your request. The default (adding no 'game' " +
                    "query parameter) will return smash4 data where applicable. If the data isn't present in Smash4, Ultimate data " +
                    "will be returned."
                });
                var apiXmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, "FrannHammer.NetCore.WebApi.xml");
                c.IncludeXmlComments(apiXmlCommentsFilePath);
            });

            return services;
        }
    }
}
