using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.NetCore.WebApi.Models;
using NUnit.Framework;
using System;
using System.Dynamic;

namespace FrannHammer.NetCore.WebApi.Tests
{
    [TestFixture]
    public class RelatedSelfLinkAttacherTests
    {
        [Test]
        public void AddsUltimateSelfLinkForUltimateGame()
        {
            var sut = new RelatedLinkSelfLinkAttacher();
            var relatedLinks = new RelatedLinks();
            relatedLinks.Ultimate = new ExpandoObject();

            sut.AddSelf(relatedLinks, Games.Ultimate, new SelfLink("testurl"));

            Assert.DoesNotThrow(() => relatedLinks.Ultimate.Self.ToString());
            Assert.That(relatedLinks.Ultimate.Self.ToString(), Is.EqualTo("testurl"));
        }

        [Test]
        public void AddsSmash4SelfLinkForSmash4Game()
        {
            var sut = new RelatedLinkSelfLinkAttacher();
            var relatedLinks = new RelatedLinks();
            relatedLinks.Smash4 = new ExpandoObject();

            sut.AddSelf(relatedLinks, Games.Smash4, new SelfLink("testurl"));

            Assert.DoesNotThrow(() => relatedLinks.Smash4.Self.ToString());
            Assert.That(relatedLinks.Smash4.Self.ToString(), Is.EqualTo("testurl"));
        }

        [Test]
        public void EnsureNullExistingRelatedLinksThrows()
        {
            var sut = new RelatedLinkSelfLinkAttacher();

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.AddSelf(null, Games.Ultimate, new SelfLink("test"));
            });
        }

        [Test]
        public void EnsureNullSelfLinkThrows()
        {
            var sut = new RelatedLinkSelfLinkAttacher();
            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.AddSelf(new RelatedLinks(), Games.Ultimate, null);
            });
        }
    }
}
