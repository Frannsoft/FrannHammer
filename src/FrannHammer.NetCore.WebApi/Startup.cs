using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.DefaultContainer.Configuration.ContainerModules;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.DataAccess;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.NetCore.WebApi.HypermediaServices.MapServices;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

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
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "KH Api", Version = "v1", Description = "Add 'game=smash4' to the end of your url to specify the game." });
                var apiXmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, "FrannHammer.NetCore.WebApi.xml");
                c.IncludeXmlComments(apiXmlCommentsFilePath);
            });

            services.AddTransient<ILinkProvider, LinkProvider>();
            services.AddTransient<IEnrichmentProvider>(s =>
            {
                return new EnrichmentProvider(
                    s.GetService<ILinkProvider>(),
                    s.GetService<IMapper>(),
                    s.GetService<LinkGenerator>(),
                    s.GetService<IActionContextAccessor>().ActionContext.HttpContext);
            });

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<WebCharacter, Character>();
                cfg.CreateMap<ICharacter, CharacterResource>();
                cfg.CreateMap<IMove, MoveResource>();
                cfg.CreateMap<IMove, UltimateMoveResource>()
                .ForMember(
                    dest => dest.BaseDamage,
                    opt => opt.MapFrom(src => new UltimateBaseDamageResourceMapService().MapFrom(src)))
                .ForMember(
                    dest => dest.HitboxActive,
                    opt => opt.MapFrom(src => new UltimateHitboxResourceMapService().MapFrom(src)));

                cfg.CreateMap<IMovement, MovementResource>();
                cfg.CreateMap<ICharacterAttributeRow, CharacterAttributeRowResource>();
                cfg.CreateMap<ICharacterAttributeName, CharacterAttributeNameResource>();
                cfg.CreateMap<IUniqueData, UniqueDataResource>();
            });

            services.AddSingleton(Mapper.Instance);

            var containerBuilder = new ContainerBuilder();

            //seed data
            var builder = new ContainerBuilder();

            builder.RegisterModule<ScrapingModule>();
            builder.RegisterType<DefaultSeeder>()
                .AsSelf();

            var container = builder.Build();
            var characterDataScraper = container.Resolve<ICharacterDataScraper>();
            var seeder = container.Resolve<DefaultSeeder>();

            var bagCharacterData = new ConcurrentBag<ICharacter>();
            var bagMoveData = new ConcurrentBag<IMove>();
            var bagMovementData = new ConcurrentBag<IMovement>();
            var bagAttributeData = new ConcurrentBag<ICharacterAttributeRow>();
            var bagUniqueData = new ConcurrentBag<IUniqueData>();

            var charactersToSeed = Characters.All.Where(c => c.DisplayName == "Ganondorf");

            Parallel.ForEach(charactersToSeed, character =>
            {
                Console.WriteLine($"Scraping data for '{character.Name}'...");
                List<string> sourceUrls = new List<string> { "http://kuroganehammer.com/Smash4/", "http://kuroganehammer.com/Ultimate/" };

                if (character.OwnerId > 58)
                {
                    sourceUrls.RemoveAt(0); //remove the smash 4 url if it's an Ultimate-only character
                }

                foreach (var sourceUrl in sourceUrls)
                {
                    var populatedCharacter = characterDataScraper.PopulateCharacterFromWeb(character, sourceUrl);

                    bagCharacterData.Add(populatedCharacter);
                    populatedCharacter.Moves.ToList().ForEach(item =>
                    {
                        bagMoveData.Add(item);
                    });

                    populatedCharacter.Movements.ToList().ForEach(item =>
                    {
                        bagMovementData.Add(item);
                    });

                    populatedCharacter.AttributeRows.ToList().ForEach(item =>
                    {
                        bagAttributeData.Add(item);
                    });

                    populatedCharacter.UniqueProperties.ToList().ForEach(item =>
                    {
                        bagUniqueData.Add(item);
                    });
                }
            });

            _characterData = bagCharacterData.ToList();
            _moveData = bagMoveData.ToList();
            _movementData = bagMovementData.ToList();
            _characterAttributeRowData = bagAttributeData.ToList();
            _uniqueData = bagUniqueData.ToList();

            services.AddSingleton(_characterData);
            services.AddSingleton(_moveData);
            services.AddSingleton(_movementData);
            services.AddSingleton(_characterAttributeRowData);
            services.AddSingleton(_uniqueData);

            //done seeding data
            containerBuilder.Populate(services);

            containerBuilder.RegisterType<InMemoryRepository<ICharacter>>()
                .As<IRepository<ICharacter>>()
                .WithParameter((pi, c) => pi.Name == "data",
                (pi, c) =>
                {
                    return c.Resolve<List<ICharacter>>();
                });

            containerBuilder.RegisterType<InMemoryRepository<IMove>>()
                .As<IRepository<IMove>>()
                .WithParameter((pi, c) => pi.Name == "data",
                (pi, c) =>
                {
                    return c.Resolve<List<IMove>>();
                });

            containerBuilder.RegisterType<InMemoryRepository<IMovement>>()
                .As<IRepository<IMovement>>()
                .WithParameter((pi, c) => pi.Name == "data",
                (pi, c) =>
                {
                    return c.Resolve<List<IMovement>>();
                });

            containerBuilder.RegisterType<InMemoryRepository<ICharacterAttributeRow>>()
                .As<IRepository<ICharacterAttributeRow>>()
                .WithParameter((pi, c) => pi.Name == "data",
                (pi, c) =>
                {
                    return c.Resolve<List<ICharacterAttributeRow>>();
                });

            containerBuilder.RegisterType<InMemoryRepository<IUniqueData>>()
                .As<IRepository<IUniqueData>>()
                .WithParameter((pi, c) => pi.Name == "data",
                (pi, c) =>
                {
                    return c.Resolve<List<IUniqueData>>();
                });

            const string RepositoryParameterName = "repository";
            const string QueryMappingParameterName = "queryMappingService";

            containerBuilder.RegisterModule<ApiServicesModule>();

            containerBuilder.RegisterType<DefaultCharacterService>()
               .As<ICharacterService>()
               .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                   (pi, c) => c.Resolve<IRepository<ICharacter>>())
                   .WithParameter((pi, c) => pi.Name == "dtoProvider",
                       (pi, c) => c.Resolve<IDtoProvider>())
                   .WithParameter((pi, c) => pi.Name == "movementService",
                       (pi, c) => c.Resolve<IMovementService>())
                   .WithParameter((pi, c) => pi.Name == "attributeRowService",
                       (pi, c) => c.Resolve<ICharacterAttributeRowService>())
                   .WithParameter((pi, c) => pi.Name == "game",
                       (pi, c) =>
                       {
                           c.Resolve<IActionContextAccessor>().ActionContext.HttpContext.Request.Query.TryGetValue("game", out StringValues game);
                           return string.IsNullOrEmpty(game) ? "Smash4" : game.ToString();
                       });

            containerBuilder.RegisterType<DefaultMoveService>()
              .As<IMoveService>()
              .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                  (pi, c) => c.Resolve<IRepository<IMove>>())
              .WithParameter((pi, c) => pi.Name == QueryMappingParameterName,
               (pi, c) => c.Resolve<IQueryMappingService>())
                .WithParameter((pi, c) => pi.Name == "game",
                       (pi, c) =>
                       {
                           c.Resolve<IActionContextAccessor>().ActionContext.HttpContext.Request.Query.TryGetValue("game", out StringValues game);
                           return string.IsNullOrEmpty(game) ? "Smash4" : game.ToString();
                       });


            containerBuilder.RegisterType<DefaultMovementService>()
                .As<IMovementService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IMovement>>())
                    .WithParameter((pi, c) => pi.Name == "game",
                       (pi, c) =>
                       {
                           c.Resolve<IActionContextAccessor>().ActionContext.HttpContext.Request.Query.TryGetValue("game", out StringValues game);
                           return string.IsNullOrEmpty(game) ? "Smash4" : game.ToString();
                       });


            containerBuilder.RegisterType<DefaultUniqueDataService>()
                .As<IUniqueDataService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IUniqueData>>())
                .WithParameter((pi, c) => pi.Name == QueryMappingParameterName,
                 (pi, c) => c.Resolve<IQueryMappingService>())
                  .WithParameter((pi, c) => pi.Name == "game",
                       (pi, c) =>
                       {
                           c.Resolve<IActionContextAccessor>().ActionContext.HttpContext.Request.Query.TryGetValue("game", out StringValues game);
                           return string.IsNullOrEmpty(game) ? "Smash4" : game.ToString();
                       });

            containerBuilder.RegisterType<DefaultCharacterAttributeService>()
              .As<ICharacterAttributeRowService>()
              .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                  (pi, c) => c.Resolve<IRepository<ICharacterAttributeRow>>())
                  .WithParameter((pi, c) => pi.Name == "characterAttributeNameProvider",
                  (pi, c) => c.Resolve<ICharacterAttributeNameProvider>())
                   .WithParameter((pi, c) => pi.Name == "game",
                       (pi, c) =>
                       {
                           c.Resolve<IActionContextAccessor>().ActionContext.HttpContext.Request.Query.TryGetValue("game", out StringValues game);
                           return string.IsNullOrEmpty(game) ? "Smash4" : game.ToString();
                       });


            containerBuilder.RegisterModule<ModelModule>();

            ApplicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            MapMongoDbConstructs();

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

        private static void MapMongoDbConstructs()
        {
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
        }
    }

    public class MongoDbBsonMapper
    {
        public void MapAllLoadedTypesDerivingFrom<T>(Assembly assemblyToReflectOver)
            where T : class
        {
            MapAllLoadedTypesDerivingFromCore<T>(assemblyToReflectOver);
        }

        public void MapAllLoadedTypesDerivingFrom<T>(IEnumerable<Assembly> assembliesToReflectOver)
            where T : class
        {
            foreach (var assembly in assembliesToReflectOver)
            {
                MapAllLoadedTypesDerivingFromCore<T>(assembly);
            }
        }

        private static void MapAllLoadedTypesDerivingFromCore<T>(Assembly assemblyToReflectOver)
        {
            Guard.VerifyObjectNotNull(assemblyToReflectOver, nameof(assemblyToReflectOver));
            BsonMapper.RegisterClassMaps(GetModelTypes<T>().ToArray());
        }

        private static IEnumerable<Type> GetModelTypes<T>()
        {
            var modelTypes =
               Assembly.GetExecutingAssembly()
                   .GetReferencedAssemblies()
                   .SelectMany(an =>
                   {
                       return Assembly.Load(an).GetExportedTypes().Where(type => type.IsSubclassOf(typeof(T)));
                   });

            return modelTypes;
        }
    }
}
