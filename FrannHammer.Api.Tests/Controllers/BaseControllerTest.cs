using System.Data.Common;
using Effort;
using FrannHammer.Api.Controllers;
using FrannHammer.Api.Models;
using FrannHammer.Services;
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

            var service = new MetadataService(DbContext);
            var smashService = new SmashAttributeTypeService(DbContext);

            CharactersController = new CharactersController(service);
            MovesController = new MovesController(service);
            MovementsController = new MovementsController(service);
            SmashAttributeTypesController = new SmashAttributeTypesController(smashService);
            CharacterAttributesController = new CharacterAttributesController(service);
            CharacterAttributeTypesController = new CharacterAttributeTypesController(service);
            NotationsController = new NotationsController(service);
            CalculatorController = new CalculatorController(DbContext);
            TestObjects = new TestObjects();

            //Startup.ConfigureAutoMapping();
        }


    }
}
