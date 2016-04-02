using System.Data.Common;
using Effort;
using KuroganeHammer.Data.Api.Controllers;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    public class BaseControllerTest
    {
        protected DbConnection Connection;
        protected ApplicationDbContext DbContext;
        protected CharactersController CharactersController;
        protected MovesController MovesController;
        protected MovementsController MovementsController;
        protected SmashAttributeTypesController SmashAttributeTypesController;
        protected CharacterAttributesController CharacterAttributesController;
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
            TestObjects = new TestObjects();
        }
    }
}
