using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class NotationControllerTest : EffortBaseTest
    {
        private NotationsController _controller;

        private Notation Post(Notation notation)
        {
            return ExecuteAndReturnCreatedAtRouteContent<Notation>(
                () => _controller.PostNotation(notation));
        }

        private Notation Get(int id)
        {
            return ExecuteAndReturnContent<Notation>(
                () => _controller.GetNotation(id));
        }

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new NotationsController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetNotations()
        {
            var notation = TestObjects.Notation();
            Post(notation);
            Get(notation.Id);
        }

        [Test]
        public void ShouldGetAllNotations()
        {
            var results = _controller.GetNotations();
            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(Notation));
        }

        [Test]
        public void ShouldAddNotation()
        {
            var notation = TestObjects.Notation();
            var result = Post(notation);
            Assert.AreEqual(notation, result);
        }

        [Test]
        public void ShouldUpdateNotation()
        {
            const string expectedName = "mewtwo";
            var notation = TestObjects.Notation();

            var dateTime = DateTime.Now;
            Thread.Sleep(100);
            //arrange
            var returnedNotation = Post(notation);
            //act
            if (returnedNotation != null)
            {
                returnedNotation.Name = expectedName;
                _controller.PutNotation(returnedNotation.Id, returnedNotation);
            }

            var updatedCharacter = Get(notation.Id);

            //assert
            Assert.That(updatedCharacter?.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter?.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteNotation()
        {
            var notation = TestObjects.Notation();
            Post(notation);

            _controller.DeleteNotation(notation.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetNotation(notation.Id));
        }
    }
}
