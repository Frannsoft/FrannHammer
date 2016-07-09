using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class AnglesControllerTest : EffortBaseTest
    {
        private AnglesController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new AnglesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            base.TestFixtureTearDown();
            _controller.Dispose();
        }

        [Test]
        public void CanGetAngleById()
        {
            var Throw = ExecuteAndReturnContent<AngleDto>(() => _controller.GetAngle(1));
            Assert.That(Throw, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoAngleFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetAngle(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllAngles()
        {
            var throws = _controller.GetAngles();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(AngleDto));
        }

        [Test]
        public void ShouldAddAngle()
        {
            var newThrow = TestObjects.Angle();
            var result = ExecuteAndReturnCreatedAtRouteContent<AngleDto>(() => _controller.PostAngle(newThrow));

            var latestThrow = _controller.GetAngles().ToList().Last();

            Assert.AreEqual(result, latestThrow); ;
        }

        [Test]
        public void ShouldUpdateAngle()
        {
            const string expectedNotes = "updated";
            var angle = ExecuteAndReturnContent<AngleDto>(() => _controller.GetAngle(1));

            //act
            if (angle != null)
            {
                angle.Notes = expectedNotes;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutAngle(angle.Id, angle));
            }

            var updatedThrow = ExecuteAndReturnContent<AngleDto>(() => _controller.GetAngle(angle.Id));

            //assert
            Assert.That(updatedThrow.Notes, Is.EqualTo(expectedNotes));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var angle = TestObjects.Angle();
            ExecuteAndReturnCreatedAtRouteContent<AngleDto>(() => _controller.PostAngle(angle));
            _controller.DeleteAngle(angle.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetAngle(angle.Id));
        }
    }
}
