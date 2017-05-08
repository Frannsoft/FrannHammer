using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FrannHammer.WebApi.Tests.Controllers
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
