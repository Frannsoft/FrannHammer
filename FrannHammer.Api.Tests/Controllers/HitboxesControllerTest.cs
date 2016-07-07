using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class HitboxsControllerTest : EffortBaseTest
    {
        private HitboxesController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new HitboxesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            base.TestFixtureTearDown();
            _controller.Dispose();
        }

        [Test]
        public void CanGetHitboxById()
        {
            var Throw = ExecuteAndReturnContent<HitboxDto>(() => _controller.GetHitbox(1));
            Assert.That(Throw, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoHitboxFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetHitbox(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllHitboxs()
        {
            var throws = _controller.GetHitboxs();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(HitboxDto));
        }

        [Test]
        public void ShouldAddHitbox()
        {
            var newThrow = TestObjects.Hitbox();
            var result = ExecuteAndReturnCreatedAtRouteContent<HitboxDto>(() => _controller.PostHitbox(newThrow));

            var latestThrow = _controller.GetHitboxs().ToList().Last();

            Assert.AreEqual(result, latestThrow); ;
        }

        [Test]
        public void ShouldUpdateHitbox()
        {
            const string expectedNotes = "updated";
            var hitbox = ExecuteAndReturnContent<HitboxDto>(() => _controller.GetHitbox(1));

            //act
            if (hitbox != null)
            {
                hitbox.Notes = expectedNotes;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutHitbox(hitbox.Id, hitbox));
            }

            var updatedThrow = ExecuteAndReturnContent<HitboxDto>(() => _controller.GetHitbox(hitbox.Id));

            //assert
            Assert.That(updatedThrow.Notes, Is.EqualTo(expectedNotes));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var hitbox = TestObjects.Hitbox();
            ExecuteAndReturnCreatedAtRouteContent<HitboxDto>(() => _controller.PostHitbox(hitbox));
            _controller.DeleteHitbox(hitbox.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetHitbox(hitbox.Id));
        }
    }
}
