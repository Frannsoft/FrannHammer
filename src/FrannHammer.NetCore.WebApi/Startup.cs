using Autofac;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.ServiceCollectionExtensions;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.PageDownloading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace FrannHammer.NetCore.WebApi
{
    public class Startup
    {
        private List<ICharacter> _characterData = new List<ICharacter>();
        private List<IMove> _moveData = new List<IMove>();
        private List<IMovement> _movementData = new List<IMovement>();
        private List<ICharacterAttributeRow> _characterAttributeRowData = new List<ICharacterAttributeRow>();
        private List<IUniqueData> _uniqueData = new List<IUniqueData>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddControllersAsServices();

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.AddSwaggerSupport();
            services.AddResourceEnrichers();
            services.AddAutoMapperSupport();
            services.AddScrapingSupport();

            services.AddSingleton(_characterData);
            services.AddSingleton(_moveData);
            services.AddSingleton(_movementData);
            services.AddSingleton(_characterAttributeRowData);
            services.AddSingleton(_uniqueData);

            services.AddRepositorySupport();
            services.AddApiServices();
            services.AddModelSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var characterDataScraper = app.ApplicationServices.GetService<ICharacterDataScraper>();
            SeedData(characterDataScraper);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KH Api");

                c.EnableDeepLinking();
            });

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    if (exception is ResourceNotFoundException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { exception.Message }));
                });
            });

            app.UseMvc();
        }

        private void SeedData(ICharacterDataScraper characterDataScraper)
        {
            var charactersToSeed = Characters.All.Where(c => c.DisplayName == "Ness");
            foreach (var character in charactersToSeed)
            {
                Console.WriteLine($"Scraping data for '{character.Name}'...");
                List<string> sourceUrls = new List<string> { "http://kuroganehammer.com/Smash4/", "http://kuroganehammer.com/Ultimate/" };

                if (character.OwnerId > 58)
                {
                    sourceUrls.RemoveAt(0); //remove the smash 4 url if it's an Ultimate-only character
                }

                foreach (var sourceUrl in sourceUrls)
                {
                    try
                    {
                        var populatedCharacter = characterDataScraper.PopulateCharacterFromWeb(character, sourceUrl);

                        _characterData.Add(populatedCharacter);
                        populatedCharacter.Moves.ToList().ForEach(item =>
                        {
                            _moveData.Add(item);
                        });

                        populatedCharacter.Movements.ToList().ForEach(item =>
                        {
                            _movementData.Add(item);
                        });

                        populatedCharacter.AttributeRows.ToList().ForEach(item =>
                        {
                            _characterAttributeRowData.Add(item);
                        });

                        populatedCharacter.UniqueProperties.ToList().ForEach(item =>
                        {
                            _uniqueData.Add(item);
                        });
                    }
                    catch (PageNotFoundException ex)
                    {
                        Console.WriteLine($"Error for '{sourceUrl}' => {ex.Message}");
                    }
                }
            }

            _characterData = _characterData.OrderBy(c => c.OwnerId).ToList();
            _moveData = _moveData.OrderBy(m => m.Name).ToList();
            _movementData = _movementData.OrderBy(m => m.Name).ToList();
            _characterAttributeRowData = _characterAttributeRowData.OrderBy(m => m.Name).ToList();
            _uniqueData = _uniqueData.OrderBy(u => u.Name).ToList();
        }
    }
}
