using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using FrannHammer.Services;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class ThrowTypesControllerTest : EffortBaseTest
    {
        private ThrowTypesController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new ThrowTypesController(_service);
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
            var throws = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetThrowTypes())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(ThrowTypeDto));
        }

        [Test]
        public void ShouldAddThrowType()
        {
            var newThrow = TestObjects.ThrowType();
            var result = ExecuteAndReturnCreatedAtRouteContent<ThrowTypeDto>(() => _controller.PostThrowType(newThrow));

            var latestThrow = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetThrowTypes()).ToList().Last();

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
