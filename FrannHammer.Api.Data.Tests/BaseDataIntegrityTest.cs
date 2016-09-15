using FrannHammer.Services;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    public class BaseDataIntegrityTest
    {
        protected Mock<IApplicationDbContext> DbContextMock;
        protected ApplicationDbContext Context { get; private set; }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            Context = new ApplicationDbContext();
        }
    }
}
