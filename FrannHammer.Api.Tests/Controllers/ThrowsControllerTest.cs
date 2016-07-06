using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

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
            var Throw = ExecuteAndReturnContent<Throw>(() => _controller.GetThrow(1));
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
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(Throw));
        }

        [Test]
        public void ShouldAddThrow()
        {
            var newThrow = TestObjects.Throw();
            var result = ExecuteAndReturnCreatedAtRouteContent<Throw>(() => _controller.PostThrow(newThrow));

            Assert.AreEqual(newThrow, result);
        }

        [Test]
        public void ShouldUpdateThrow()
        {
            const bool expectedBool = false;
            var Throw = TestObjects.Throw();

            var dateTime = DateTime.Now;
            Thread.Sleep(100);
            //arrange
            var returnedThrow =
                ExecuteAndReturnCreatedAtRouteContent<Throw>(() => _controller.PostThrow(Throw));
            //act
            if (returnedThrow != null)
            {
                returnedThrow.WeightDependent = expectedBool;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutThrow(returnedThrow.Id, returnedThrow));
            }

            var updatedThrow = ExecuteAndReturnContent<Throw>(() => _controller.GetThrow(Throw.Id));

            //assert
            Assert.That(updatedThrow.WeightDependent, Is.EqualTo(expectedBool));
            Assert.That(updatedThrow.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var Throw = TestObjects.Throw();
            ExecuteAndReturnCreatedAtRouteContent<Throw>(() => _controller.PostThrow(Throw));
            ExecuteAndReturnContent<Throw>(() => _controller.DeleteThrow(Throw.Id));
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetThrow(Throw.Id));
        }

    }
}
