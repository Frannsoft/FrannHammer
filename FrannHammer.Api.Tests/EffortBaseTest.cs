using System;
using System.Data.Common;
using Effort.DataLoaders;
using FrannHammer.Api.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests
{
    public abstract class EffortBaseTest
    {
        protected ApplicationDbContext Context;

        private DbConnection _connection;

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            IDataLoader loader = new CsvDataLoader($"{path}/fakeDb");

            _connection = Effort.DbConnectionFactory.CreateTransient(loader);
            Context = new ApplicationDbContext(_connection);
        }

        [TestFixtureTearDown]
        public virtual void TestFixtureTearDown()
        {
            _connection.Close();
            Context.Dispose();
        }
    }
}
