using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Effort;
using KuroganeHammer.Data.Api.Controllers;
using KuroganeHammer.Data.Api.Models;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    public abstract class BaseControllerTest
    {
        protected TestServer _server;

        protected DbConnection Connection;
        protected ApplicationDbContext DbContext;
        protected CharactersController CharactersController;
        protected MovesController MovesController;
        protected MovementsController MovementsController;
        protected SmashAttributeTypesController SmashAttributeTypesController;
        protected CharacterAttributesController CharacterAttributesController;
        protected CharacterAttributeTypesController CharacterAttributeTypesController;
        protected NotationsController NotationsController;
        protected CalculatorController CalculatorController;
        protected TestObjects TestObjects;

        [SetUp]
        public void Setup()
        {
            Connection = DbConnectionFactory.CreateTransient();
            DbContext = new ApplicationDbContext(Connection);
            CharactersController = new CharactersController(DbContext);
            MovesController = new MovesController(DbContext);
            MovementsController = new MovementsController(DbContext);
            SmashAttributeTypesController = new SmashAttributeTypesController(DbContext);
            CharacterAttributesController = new CharacterAttributesController(DbContext);
            CharacterAttributeTypesController = new CharacterAttributeTypesController(DbContext);
            NotationsController = new NotationsController(DbContext);
            CalculatorController = new CalculatorController(DbContext);
            TestObjects = new TestObjects();

            _server = TestServer.Create(app =>
            {
                var startup = new Startup();
                app.UseCors(CorsOptions.AllowAll);
                startup.ConfigureAuth(app);

                var config = new HttpConfiguration();
                WebApiConfig.Register(config);

                app.UseWebApi(config);
            });

            PostSetup(_server);
        }

        [TearDown]
        public void TearDown()
        {
            _server?.Dispose();
        }

        protected virtual void PostSetup(TestServer server)
        { }

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
    }
}
