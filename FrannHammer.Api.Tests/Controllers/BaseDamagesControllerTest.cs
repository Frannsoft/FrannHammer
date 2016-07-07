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
    public class BaseDamagesControllerTest : EffortBaseTest
    {
        private BaseDamagesController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new BaseDamagesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            base.TestFixtureTearDown();
            _controller.Dispose();
        }

        [Test]
        public void CanGetBaseDamageById()
        {
            var Throw = ExecuteAndReturnContent<BaseDamageDto>(() => _controller.GetBaseDamage(1));
            Assert.That(Throw, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoBaseDamageFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetBaseDamage(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllBaseDamages()
        {
            var throws = _controller.GetBaseDamages();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(BaseDamageDto));
        }

        [Test]
        public void ShouldAddBaseDamage()
        {
            var newThrow = TestObjects.BaseDamage();
            var result = ExecuteAndReturnCreatedAtRouteContent<BaseDamageDto>(() => _controller.PostBaseDamage(newThrow));

            var latestThrow = _controller.GetBaseDamages().ToList().Last();

            Assert.AreEqual(result, latestThrow); ;
        }

        [Test]
        public void ShouldUpdateBaseDamage()
        {
            const string expectedNotes = "updated";
            var baseDamage = ExecuteAndReturnContent<BaseDamageDto>(() => _controller.GetBaseDamage(1));

            //act
            if (baseDamage != null)
            {
                baseDamage.Notes = expectedNotes;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutBaseDamage(baseDamage.Id, baseDamage));
            }

            var updatedThrow = ExecuteAndReturnContent<BaseDamageDto>(() => _controller.GetBaseDamage(baseDamage.Id));

            //assert
            Assert.That(updatedThrow.Notes, Is.EqualTo(expectedNotes));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var baseDamage = TestObjects.BaseDamage();
            ExecuteAndReturnCreatedAtRouteContent<BaseDamageDto>(() => _controller.PostBaseDamage(baseDamage));
            _controller.DeleteBaseDamage(baseDamage.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetBaseDamage(baseDamage.Id));
        }
    }
}
