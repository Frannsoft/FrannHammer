using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FrannHammer.Api.Services.Tests
{
    public abstract class BaseServiceTests
    {
        protected Fixture Fixture { get; }

        protected BaseServiceTests()
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
