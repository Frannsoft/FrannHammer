using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Effort.DataLoaders;
using FrannHammer.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using Owin;

namespace FrannHammer.Api.Tests.Controllers
{
    public abstract class BaseAuthenticationTest : BaseControllerTest
    {
        private TestServer _server;
        private IContainer _container;

        protected virtual string Username => "GETuser";
        protected virtual string Password => "GETpassword";

        protected string Token;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            //start the server
            _server = TestServer.Create(app =>
            {
                var startup = new Startup();
                app.UseCors(CorsOptions.AllowAll);

                var config = new HttpConfiguration();
                Register(config);
                app.UseAutofacMiddleware(_container);

                startup.ConfigureAuth(app, _container);

                app.UseAutofacWebApi(config);
                app.UseWebApi(config);
            });
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _server?.Dispose();
        }

        protected virtual async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _server.CreateRequest(requestUri).GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostAsync<TModel>(string requestUri, TModel model)
        {
            return await _server.CreateRequest(requestUri)
                .And(
                    request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .PostAsync();
        }

        protected virtual async Task<HttpResponseMessage> PutAsync<TModel>(string requestUri, TModel model)
        {
            var content = new ObjectContent<TModel>(model, new JsonMediaTypeFormatter());
            return await _server.HttpClient.PutAsync(requestUri, content);
        }

        protected virtual async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await _server.HttpClient.DeleteAsync(requestUri);
        }

        private void Register(HttpConfiguration config)
        {
            //// Web API configuration and services
            //// Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var builder = new ContainerBuilder();

            var path = AppDomain.CurrentDomain.BaseDirectory;
            IDataLoader loader = new CsvDataLoader($"{path}/fakeDb");

            var connection = Effort.DbConnectionFactory.CreateTransient(loader);

            builder.RegisterInstance(new ApplicationDbContext(connection));

            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>()))
                .As<IUserStore<ApplicationUser>>();

            var testContext = new ApplicationDbContext(connection);
            var assembly = typeof(Startup).Assembly;
            builder.RegisterApiControllers(assembly).WithParameter("context", testContext);

            _container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        protected void Login()
        {
            var tokenDetails = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", Username),
                new KeyValuePair<string, string>("password", Password)
            });

            var tokenResult = _server.HttpClient.PostAsync("api/token", tokenDetails).Result;

            Assert.That(tokenResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            Token = (string)body["access_token"];

            Assert.That(Token, Is.Not.Empty);
        }
    }
}
