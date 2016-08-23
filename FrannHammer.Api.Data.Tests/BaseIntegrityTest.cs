using FrannHammer.Services;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    public class BaseIntegrityTest
    {
        protected Mock<IApplicationDbContext> DbContextMock;
        protected ApplicationDbContext Context;

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            Startup.ConfigureAutoMapping();
            Context = new ApplicationDbContext();
        }
    }
}
