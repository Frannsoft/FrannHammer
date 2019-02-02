using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.ServiceCollectionExtensions;
using FrannHammer.WebScraping.Contracts.Character;
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
                c.SwaggerEndpoint("/swagger/v7/swagger.json", "KH Api");
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
#if !DEBUG
            var charactersToSeed = Characters.All;
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
                        _moveData.AddRange(populatedCharacter.Moves);
                        _movementData.AddRange(populatedCharacter.Movements);
                        _characterAttributeRowData.AddRange(populatedCharacter.AttributeRows);
                        _uniqueData.AddRange(populatedCharacter.UniqueProperties);
                    }
                    catch (PageNotFoundException ex)
                    {
                        Console.WriteLine($"Error for '{character.Name}' at '{sourceUrl}' => {ex.Message}");
                    }
                }
            }
#endif
#if DEBUG
            _characterData.AddRange(JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText("../../../../../localstorage/character.json")));
            _moveData.AddRange(JsonConvert.DeserializeObject<List<Move>>(File.ReadAllText("../../../../../localstorage/move.json")));
            _movementData.AddRange(JsonConvert.DeserializeObject<List<Movement>>(File.ReadAllText("../../../../../localstorage/movement.json")));
            _characterAttributeRowData.AddRange(JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText("../../../../../localstorage/attribute.json")).Select(attr =>
            {
                return new CharacterAttributeRow
                {
                    Game = attr["Game"],
                    InstanceId = attr["InstanceId"],
                    Name = attr["Name"],
                    Owner = attr["Owner"],
                    OwnerId = attr["OwnerId"],
                    Values = attr["Values"] as List<IAttribute>
                };
            }));
            _uniqueData.AddRange(JsonConvert.DeserializeObject<List<UniqueData>>(File.ReadAllText("../../../../../localstorage/unique.json")));
#endif

            _characterData.Sort((c1, c2) => c1.OwnerId.CompareTo(c2.OwnerId));
            _moveData.Sort((m1, m2) => m1.Name.CompareTo(m2.Name));
            _movementData.Sort((m1, m2) => m1.Name.CompareTo(m2.Name));
            _characterAttributeRowData.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            _uniqueData.Sort((u1, u2) => u1.Name.CompareTo(u2.Name));

            //write pulled data to local
            //File.WriteAllText("character.json", JsonConvert.SerializeObject(_characterData));
            //File.WriteAllText("move.json", JsonConvert.SerializeObject(_moveData));
            //File.WriteAllText("movement.json", JsonConvert.SerializeObject(_movementData));
            //File.WriteAllText("attribute.json", JsonConvert.SerializeObject(_characterAttributeRowData));
            //File.WriteAllText("unique.json", JsonConvert.SerializeObject(_uniqueData));
            //done writing to local
        }
    }
}
