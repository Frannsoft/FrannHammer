using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using FrannHammer.Api;
using FrannHammer.Api.Models;
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
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app, Container.Instance.AutoFacContainer);
        }
    }
}
