using AutoFixture;
using NUnit.Framework;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    public abstract class BaseControllerTests
    {
        protected Fixture Fixture { get; }

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
