using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class ThrowsControllerTest : EffortBaseTest
    {
        private ThrowsController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new ThrowsController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            base.TestFixtureTearDown();
            _controller.Dispose();
        }

        [Test]
        public void CanGetThrowById()
        {
            var Throw = ExecuteAndReturnContent<ThrowDto>(() => _controller.GetThrow(1));
            Assert.That(Throw, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoThrowFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetThrow(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllThrows()
        {
            var throws = _controller.GetThrows();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(ThrowDto));
        }

        [Test]
        public void ShouldAddThrow()
        {
            var newThrow = TestObjects.Throw();
            var result = ExecuteAndReturnCreatedAtRouteContent<ThrowDto>(() => _controller.PostThrow(newThrow));

            var latestThrow = _controller.GetThrows().ToList().Last();

            Assert.AreEqual(result, latestThrow);
        }

        [Test]
        public void ShouldUpdateThrow()
        {
            const bool expectedBool = false;
            var existingThrow = ExecuteAndReturnContent<ThrowDto>(() => _controller.GetThrow(1));

            Thread.Sleep(100);
            //arrange
            var returnedThrow =
                ExecuteAndReturnCreatedAtRouteContent<ThrowDto>(() => _controller.PostThrow(existingThrow));

            //act
            if (returnedThrow != null)
            {
                returnedThrow.WeightDependent = expectedBool;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutThrow(returnedThrow.Id, returnedThrow));
            }

            var updatedThrow = ExecuteAndReturnContent<ThrowDto>(() => _controller.GetThrow(existingThrow.Id));

            //assert
            Assert.That(updatedThrow.WeightDependent, Is.EqualTo(expectedBool));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var Throw = TestObjects.Throw();
            ExecuteAndReturnCreatedAtRouteContent<ThrowDto>(() => _controller.PostThrow(Throw));
            _controller.DeleteThrow(Throw.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetThrow(Throw.Id));
        }

    }
}
