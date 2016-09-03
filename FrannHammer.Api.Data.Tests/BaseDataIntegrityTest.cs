using FrannHammer.Services;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    public class BaseDataIntegrityTest
    {
        protected Mock<IApplicationDbContext> DbContextMock;
        protected ApplicationDbContext Context;

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            Context = new ApplicationDbContext();
        }
    }
}
