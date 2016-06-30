using System.Web.Http;
using System.Web.Mvc;
using FrannHammer.Api;
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
        }
    }
}
