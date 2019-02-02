using AutoFixture;
using NUnit.Framework;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    public abstract class BaseControllerTests
    {
        protected Fixture Fixture { get; }
        protected static IntegrationTestServer TestServer { get; } = new IntegrationTestServer();

        protected BaseControllerTests()
        {
            Fixture = new Fixture();
        }

        [SetUp]
        public virtual void SetUp()
        {
            //don't register any types to the fixture by default
        }
    }
}
