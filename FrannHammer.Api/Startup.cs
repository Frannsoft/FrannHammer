using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using FrannHammer.Api;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace FrannHammer.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app, Container.Instance.AutoFacContainer);

            ConfigureAutoMapping();
        }

        internal static void ConfigureAutoMapping()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Character, CharacterDto>()
               .ConstructProjectionUsing(c => new CharacterDto(c.Name)));//("name", x => x.MapFrom(c => c.Name)));

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
