﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using FrannHammer.Services;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class KnockbackGrowthsControllerTest : EffortBaseTest
    {
        private KnockbackGrowthsController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new KnockbackGrowthsController(_service);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            base.TestFixtureTearDown();
            _controller.Dispose();
        }

        [Test]
        public void CanGetKnockbackGrowthById()
        {
            var Throw = ExecuteAndReturnContent<KnockbackGrowthDto>(() => _controller.GetKnockbackGrowth(1));
            Assert.That(Throw, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoKnockbackGrowthFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetKnockbackGrowth(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllKnockbackGrowths()
        {
            var throws = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetKnockbackGrowths())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(KnockbackGrowthDto));
        }

        [Test]
        public void ShouldAddKnockbackGrowth()
        {
            var newThrow = TestObjects.KnockbackGrowth();
            var result = ExecuteAndReturnCreatedAtRouteContent<KnockbackGrowthDto>(() => _controller.PostKnockbackGrowth(newThrow));

            var latestThrow = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetKnockbackGrowths()).ToList().Last();

            Assert.AreEqual(result, latestThrow); ;
        }

        [Test]
        public void ShouldUpdateKnockbackGrowth()
        {
            const string expectedNotes = "updated";
            var knockbackGrowth = ExecuteAndReturnContent<KnockbackGrowthDto>(() => _controller.GetKnockbackGrowth(1));

            //act
            if (knockbackGrowth != null)
            {
                knockbackGrowth.Notes = expectedNotes;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutKnockbackGrowth(knockbackGrowth.Id, knockbackGrowth));
            }

            var updatedThrow = ExecuteAndReturnContent<KnockbackGrowthDto>(() => _controller.GetKnockbackGrowth(knockbackGrowth.Id));

            //assert
            Assert.That(updatedThrow.Notes, Is.EqualTo(expectedNotes));
        }

        [Test]
        public void ShouldDeleteThrow()
        {
            var knockbackGrowth = TestObjects.KnockbackGrowth();
            ExecuteAndReturnCreatedAtRouteContent<KnockbackGrowthDto>(() => _controller.PostKnockbackGrowth(knockbackGrowth));
            _controller.DeleteKnockbackGrowth(knockbackGrowth.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetKnockbackGrowth(knockbackGrowth.Id));
        }
    }
}
