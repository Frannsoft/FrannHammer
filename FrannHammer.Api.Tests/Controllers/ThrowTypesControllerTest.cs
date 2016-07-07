using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class ThrowTypesControllerTest : EffortBaseTest
    {
        private ThrowTypesController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new ThrowTypesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            base.TestFixtureTearDown();
            _controller.Dispose();
        }

        [Test]
        public void CanGetThrowTypeById()
        {
            var Throw = ExecuteAndReturnContent<ThrowTypeDto>(() => _controller.GetThrowType(1));
            Assert.That(Throw, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoThrowTypeFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetThrowType(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllThrowTypes()
        {
            var throws = _controller.GetThrowTypes();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(ThrowTypeDto));
        }

        [Test]
        public void ShouldAddThrowType()
        {
            var newThrow = TestObjects.ThrowType();
            var result = ExecuteAndReturnCreatedAtRouteContent<ThrowTypeDto>(() => _controller.PostThrowType(newThrow));

            var latestThrow = _controller.GetThrowTypes().ToList().Last();

            Assert.AreEqual(result, latestThrow); ;
        }

        [Test]
        public void ShouldUpdateThrowType()
        {
            const string expectedName = "updated";
            var throwType = ExecuteAndReturnContent<ThrowTypeDto>(() => _controller.GetThrowType(1));

            //act
            if (throwType != null)
            {
                throwType.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutThrowType(throwType.Id, throwType));
            }

            var updatedThrow = ExecuteAndReturnContent<ThrowTypeDto>(() => _controller.GetThrowType(throwType.Id));

            //assert
            Assert.That(updatedThrow.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var throwType = TestObjects.ThrowType();
            ExecuteAndReturnCreatedAtRouteContent<ThrowTypeDto>(() => _controller.PostThrowType(throwType));
            _controller.DeleteThrowType(throwType.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetThrowType(throwType.Id));
        }
    }
}
