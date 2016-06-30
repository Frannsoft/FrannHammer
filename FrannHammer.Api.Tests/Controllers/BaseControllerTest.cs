using System.Data.Common;
using Effort;
using FrannHammer.Api.Controllers;
using FrannHammer.Api.Models;
using NUnit.Framework;

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

            Startup.ConfigureAutoMapping();
        }


    }
}
