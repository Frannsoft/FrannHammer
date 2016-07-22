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
    public class BaseDamagesControllerTest : EffortBaseTest
    {
        private BaseDamagesController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new BaseDamagesController(_service);
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
            var baseDamage = ExecuteAndReturnDynamic(() => _controller.GetBaseDamage(1));
            Assert.That(baseDamage, Is.Not.Null);
        }

        //[Test]
        //public void NotFoundResultWhenNoBaseDamageFoundById()
        //{
        //    var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetBaseDamage(0));
        //    Assert.That(result, Is.Not.Null);
        //}

        [Test]
        public void ShouldGetAllBaseDamages()
        {
            var baseDamages = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetBaseDamages())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(baseDamages);
            CollectionAssert.AllItemsAreUnique(baseDamages);
        }

        [Test]
        public void ShouldAddBaseDamage()
        {
            var newBaseDamage = TestObjects.BaseDamage();
            var result = ExecuteAndReturnCreatedAtRouteContent<BaseDamageDto>(() => _controller.PostBaseDamage(newBaseDamage));
            var latest = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetBaseDamages()).ToList().Last();

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
        public void ShouldUpdateBaseDamage()
        {
            const string expectedNotes = "updated";
            var result = ExecuteAndReturnDynamic(() => _controller.GetBaseDamage(1));

            var json = JsonConvert.SerializeObject(result);
            var baseDamage = JsonConvert.DeserializeObject<BaseDamageDto>(json);

            //act
            if (baseDamage != null)
            {
                baseDamage.Notes = expectedNotes;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutBaseDamage(baseDamage.Id, baseDamage));
            }

            var updatedThrow = ExecuteAndReturnDynamic(() => _controller.GetBaseDamage(baseDamage.Id));

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
