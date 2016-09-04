using FrannHammer.Api;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public abstract class ServiceBaseTest
    {
        protected ApplicationDbContext Context;

        [SetUp]
        public virtual void SetUp()
        {
            Startup.ConfigureAutoMapping();
            Context = new ApplicationDbContext();
        }
    }
}
