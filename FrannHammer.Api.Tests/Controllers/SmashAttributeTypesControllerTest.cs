using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class SmashAttributeTypesControllerTest : EffortBaseTest
    {
        private SmashAttributeTypesController _controller;

        private SmashAttributeTypeDto Post(SmashAttributeTypeDto smashAttributeType)
        {
            return ExecuteAndReturnCreatedAtRouteContent<SmashAttributeTypeDto>(
                () => _controller.PostSmashAttributeType(smashAttributeType));
        }

        private SmashAttributeTypeDto Get(int id)
        {
            return ExecuteAndReturnContent<SmashAttributeTypeDto>(
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
        public void ShouldGetSmashAttributeType()
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
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(SmashAttributeTypeDto));
        }

        [Test]
        public void ShouldAddSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            var result = Post(smashAttributeType);

            var latestSmashAttributeType = _controller.GetSmashAttributeTypes().ToList().Last();

            Assert.AreEqual(result, latestSmashAttributeType);
        }

        [Test]
        public void ShouldUpdateSmashAttributeTypes()
        {
            const string expectedName = "new name";
            var smashAttributeType = _controller.GetSmashAttributeTypes().ToList().First();

            if (smashAttributeType != null)
            {
                smashAttributeType.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutSmashAttributeType(smashAttributeType.Id, smashAttributeType));
            }

            var updatedSmashAttributeTypes = Get(smashAttributeType.Id);

            Assert.That(updatedSmashAttributeTypes?.Name, Is.EqualTo(expectedName));
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
