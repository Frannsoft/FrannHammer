using System;
using Effort;
using Effort.DataLoaders;
using FrannHammer.Api;
using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public abstract class ServiceBaseTest
    {
        protected const string Id = "id";
        protected const string Name = "Name";
        protected const string DisplayName = "displayname";
        protected const string WeightDependent = "weightdependent";
        protected const string OwnerId = "ownerid";
        protected const string Value = "value";
        protected const string MoveName = "movename";
        protected const string Hitbox1 = "hitbox1";
        protected const string Hitbox2 = "hitbox2";
        protected const string RawValue = "rawvalue";
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
