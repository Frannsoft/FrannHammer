using System;
using System.Data.Common;
using Effort.DataLoaders;
using FrannHammer.Api;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public abstract class ServiceBaseTest
    {
        private DbConnection _connection;

        protected Mock<IApplicationDbContext> DbContextMock;
        protected ApplicationDbContext Context;

        [SetUp]
        public virtual void SetUp()
        {
            Startup.ConfigureAutoMapping();

            var path = AppDomain.CurrentDomain.BaseDirectory;
            IDataLoader loader = new CsvDataLoader($"{path}\\fakeDb\\");

            _connection = Effort.DbConnectionFactory.CreateTransient(loader);
            Context = new ApplicationDbContext(_connection);
        }


    }
}
