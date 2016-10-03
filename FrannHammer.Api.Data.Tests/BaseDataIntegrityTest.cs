using System;
using Effort;
using Effort.DataLoaders;
using FrannHammer.Services;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    public class BaseDataIntegrityTest
    {
        protected ApplicationDbContext Context { get; private set; }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            var dataLoader = new CsvDataLoader(AppDomain.CurrentDomain.BaseDirectory + "\\fakeDb\\");
            var connection = DbConnectionFactory.CreatePersistent("testdb", dataLoader);
            Context = new ApplicationDbContext(connection);
        }
    }
}
