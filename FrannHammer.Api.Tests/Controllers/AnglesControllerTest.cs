using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using FrannHammer.Services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class AnglesControllerTest : EffortBaseTest
    {
        private AnglesController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new AnglesController(_service);
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
            var Throw = ExecuteAndReturnDynamic(() => _controller.GetAngle(1));
            Assert.That(Throw, Is.Not.Null);
        }

        //[Test]
        //public void NotFoundResultWhenNoAngleFoundById()
        //{
        //    var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetAngle(0));
        //    Assert.That(result, Is.Not.Null);
        //}

        [Test]
        public void ShouldGetAllAngles()
        {
            var throws = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetAngles())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
        }

        [Test]
        public void ShouldAddAngle()
        {
            var newThrow = TestObjects.Angle();
            var result = ExecuteAndReturnCreatedAtRouteContent<AngleDto>(() => _controller.PostAngle(newThrow));

            var latest = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetAngles()).Last();

            Assert.AreEqual(result.Hitbox1, latest.Hitbox1);
            Assert.AreEqual(result.Hitbox2, latest.Hitbox2);
            Assert.AreEqual(result.Hitbox3, latest.Hitbox3);
            Assert.AreEqual(result.Hitbox4, latest.Hitbox4);
            Assert.AreEqual(result.Hitbox5, latest.Hitbox5);
            Assert.AreEqual(result.Hitbox6, latest.Hitbox6);
            Assert.AreEqual(result.Id, latest.Id);
            Assert.AreEqual(result.Notes, latest.Notes);
            Assert.AreEqual(result.OwnerId, latest.OwnerId);
            Assert.AreEqual(result.RawValue, latest.RawValue);
        }

        [Test]
        public void ShouldUpdateAngle()
        {
            const string expectedNotes = "updated";
            var result = ExecuteAndReturnDynamic(() => _controller.GetAngle(1));

            var json = JsonConvert.SerializeObject(result);
            var angle = JsonConvert.DeserializeObject<AngleDto>(json);

            //act
            if (angle != null)
            {
                angle.Notes = expectedNotes;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutAngle(angle.Id, angle));
            }

            var updatedThrow = ExecuteAndReturnDynamic(() => _controller.GetAngle(angle.Id));

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
