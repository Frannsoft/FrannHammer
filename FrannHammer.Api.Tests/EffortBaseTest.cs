using System;
using System.Data.Common;
using System.Dynamic;
using System.Web.Http;
using System.Web.Http.Results;
using Effort.DataLoaders;
using FrannHammer.Services;
using NUnit.Framework;

namespace FrannHammer.Api.Tests
{
    public abstract class EffortBaseTest
    {
        protected ApplicationDbContext Context;
        protected TestObjects TestObjects;

        private DbConnection _connection;

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            IDataLoader loader = new CsvDataLoader($"{path}fakeDb\\");

            _connection = Effort.DbConnectionFactory.CreateTransient(loader);
            Context = new ApplicationDbContext(_connection);
            TestObjects = new TestObjects();
            Startup.ConfigureAutoMapping();
        }

        [TestFixtureTearDown]
        public virtual void TestFixtureTearDown()
        {
            _connection.Close();
            Context?.Dispose();
        }

        protected T ExecuteAndReturn<T>(Func<IHttpActionResult> op)
            where T : class, IHttpActionResult
        {
            var response = op();
            var retVal = response as T;

            Assert.That(retVal, Is.Not.Null);

            return retVal;
        }

        protected dynamic ExecuteAndReturnDynamic(Func<IHttpActionResult> op)
        {
            var response = op();
            var retVal = response as OkNegotiatedContentResult<ExpandoObject>;

            Assert.That(retVal, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(retVal.Content, Is.Not.Null);

            return retVal.Content;
        }

        protected T ExecuteAndReturnContent<T>(Func<IHttpActionResult> op)
        {
            var response = op();
            var retVal = response as OkNegotiatedContentResult<T>;

            Assert.That(retVal, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(retVal.Content, Is.Not.Null);

            return retVal.Content;
        }

        protected dynamic ExecuteAndReturnExpandoObject(Func<IHttpActionResult> op)
        {
            var response = op();
            var retVal = response as OkNegotiatedContentResult<ExpandoObject>;

            Assert.That(retVal, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(retVal.Content, Is.Not.Null);

            return retVal.Content;
        }

        protected T ExecuteAndReturnCreatedAtRouteContent<T>(Func<IHttpActionResult> op)
        {
            var response = op();
            var retVal = response as CreatedAtRouteNegotiatedContentResult<T>;

            Assert.That(retVal, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(retVal.Content, Is.Not.Null);
            return retVal.Content;
        }
    }
}
