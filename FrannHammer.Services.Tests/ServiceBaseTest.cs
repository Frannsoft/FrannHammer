using System;
using Effort;
using Effort.DataLoaders;
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

            var dataLoader = new CsvDataLoader(AppDomain.CurrentDomain.BaseDirectory + "\\fakeDb\\");
            var connection = DbConnectionFactory.CreatePersistent("testdb", dataLoader);

            Context = new ApplicationDbContext(connection);
            ResultValidationService = new ResultValidationService();
        }
    }
}
