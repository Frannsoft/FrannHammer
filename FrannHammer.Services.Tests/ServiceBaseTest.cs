using FrannHammer.Api;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public abstract class ServiceBaseTest
    {
        protected ApplicationDbContext Context;
        protected IResultValidationService ResultValidationService;

        [SetUp]
        public virtual void SetUp()
        {
            Startup.ConfigureAutoMapping();
            Context = new ApplicationDbContext();
            ResultValidationService = new ResultValidationService();
        }
    }
}
