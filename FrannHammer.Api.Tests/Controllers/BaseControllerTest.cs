using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Effort;
using FrannHammer.Api.Controllers;
using FrannHammer.Api.Models;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

namespace FrannHammer.Api.Tests.Controllers
{
    public abstract class BaseControllerTest
    {

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
        }


    }
}
