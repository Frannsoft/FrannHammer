using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class SmashAttributeTypesControllerTest : EffortBaseTest
    {
        private SmashAttributeTypesController _controller;

        private SmashAttributeType Post(SmashAttributeType smashAttributeType)
        {
            return ExecuteAndReturnCreatedAtRouteContent<SmashAttributeType>(
                () => _controller.PostSmashAttributeType(smashAttributeType));
        }

        private SmashAttributeType Get(int id)
        {
            return ExecuteAndReturnContent<SmashAttributeType>(
                () => _controller.GetSmashAttributeType(id));
        }

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new SmashAttributeTypesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            Post(smashAttributeType);
            Get(smashAttributeType.Id);
        }

        [Test]
        public void ShouldGetAllSmashAttributeTypes()
        {
            var results = _controller.GetSmashAttributeTypes();
            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(SmashAttributeType));
        }

        [Test]
        public void ShouldAddSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            var result = Post(smashAttributeType);

            Assert.AreEqual(smashAttributeType, result);
        }

        [Test]
        public void ShouldUpdateSmashAttributeTypes()
        {
            const string expectedName = "new name";
            var smashAttributeType = TestObjects.SmashAttributeType();

            var dateTime = DateTime.Now;
            Thread.Sleep(100);
            var returnedSmashAttributeTypes = Post(smashAttributeType);

            if (returnedSmashAttributeTypes != null)
            {
                returnedSmashAttributeTypes.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutSmashAttributeType(returnedSmashAttributeTypes.Id, returnedSmashAttributeTypes));
            }

            var updatedSmashAttributeTypes = Get(smashAttributeType.Id);

            Assert.That(updatedSmashAttributeTypes?.Name, Is.EqualTo(expectedName));
            Assert.That(updatedSmashAttributeTypes?.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            Post(smashAttributeType);

            _controller.DeleteSmashAttributeType(smashAttributeType.Id);

            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetSmashAttributeType(smashAttributeType.Id));
            Assert.That(result, Is.Not.Null);
        }
    }
}
