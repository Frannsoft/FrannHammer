using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultMoveScrapingServiceIntegrationTests
    {
        private IMoveScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var moveProvider = new DefaultMoveProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);

            _scrapingServices = new DefaultMoveScrapingServices(moveProvider, webServices);
        }

        private static void AssertMoveIsValid(IMove move, WebCharacter character)
        {
            Assert.That(move, Is.Not.Null);
            Assert.That(move.Name, Is.Not.Null);
            Assert.That(move.Angle, Is.Not.Null);
            Assert.That(move.BaseDamage, Is.Not.Null);
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null);
            Assert.That(move.FirstActionableFrame, Is.Not.Null);
            Assert.That(move.HitboxActive, Is.Not.Null);
            Assert.That(move.KnockbackGrowth, Is.Not.Null);
            Assert.That(move.Owner, Is.EqualTo(character.Name), $"{nameof(move.Owner)}");
        }

        private static IEnumerable<WebCharacter> TestCharacters()
        {
            yield return Characters.Cloud;
            yield return Characters.Yoshi;
            yield return Characters.Greninja;
            yield return Characters.Bowser;
        }

        [Test]
        public void ScrapeGroundMoves_HeaderRowsAreExcludeFromScrapedValues()
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);

            var groundMoves = groundMoveScrapingService.Scrape(Characters.Greninja).ToList();

            CollectionAssert.AllItemsAreNotNull(groundMoves);
            CollectionAssert.AllItemsAreUnique(groundMoves);
            CollectionAssert.IsNotEmpty(groundMoves);

            groundMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, Characters.Greninja);
            });

            Assert.That(!groundMoves.Any(move => move.Name.Equals(ScrapingConstants.ExcludedRowHeaders.Grabs, StringComparison.OrdinalIgnoreCase)),
                $"Should not contain '{ScrapingConstants.ExcludedRowHeaders.Grabs}'");

            Assert.That(!groundMoves.Any(move => move.Name.Equals(ScrapingConstants.ExcludedRowHeaders.Throws, StringComparison.OrdinalIgnoreCase)),
                $"Should not contain '{ScrapingConstants.ExcludedRowHeaders.Throws}'");

            Assert.That(!groundMoves.Any(move => move.Name.Equals(ScrapingConstants.ExcludedRowHeaders.Miscellaneous, StringComparison.OrdinalIgnoreCase)),
                $"Should not contain '{ScrapingConstants.ExcludedRowHeaders.Miscellaneous}'");
        }

        [Test]
        public void ScrapeGroundMovesForCharacter()
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);

            var groundMoves = groundMoveScrapingService.Scrape(Characters.Greninja).ToList();

            CollectionAssert.AllItemsAreNotNull(groundMoves);
            CollectionAssert.AllItemsAreUnique(groundMoves);
            CollectionAssert.IsNotEmpty(groundMoves);

            groundMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, Characters.Greninja);
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void GroundMoveScraperShouldExcludeThrowMoves(WebCharacter character)
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);
            var groundMoves = groundMoveScrapingService.Scrape(character).ToList();

            CollectionAssert.AllItemsAreNotNull(groundMoves);
            CollectionAssert.AllItemsAreUnique(groundMoves);
            CollectionAssert.IsNotEmpty(groundMoves);

            groundMoves.ForEach(move =>
            {
                Assert.That(move.Name.IndexOf(ScrapingConstants.CommonMoveNames.Throw, StringComparison.OrdinalIgnoreCase) == -1,
                    $"{nameof(IMove.Name)} should not contain {ScrapingConstants.CommonMoveNames.Throw}.  This means the scraper might be pulling in throw moves.");
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void GroundMoveScraperShouldIncludeGrabs(WebCharacter character)
        {
            const int expectedGrabCount = 3;

            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);
            var groundMoves = groundMoveScrapingService.Scrape(character).ToList();

            int actualNumberOfGrabMoves = groundMoves.Count(move => move.Name.EndsWith(ScrapingConstants.CommonMoveNames.Grab, StringComparison.OrdinalIgnoreCase));

            Assert.That(actualNumberOfGrabMoves, Is.EqualTo(expectedGrabCount), $"{character.Name}");
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void GroundMoveScraperShouldExcludeRowHeaders(WebCharacter character)
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);
            var groundMoves = groundMoveScrapingService.Scrape(character).ToList();

            groundMoves.ForEach(move =>
            {
                Assert.That(move.Name, Is.Not.EqualTo(ScrapingConstants.ExcludedRowHeaders.Grabs), $"{nameof(character.Name)}");
                Assert.That(move.Name, Is.Not.EqualTo(ScrapingConstants.ExcludedRowHeaders.Throws), $"{nameof(character.Name)}");
                Assert.That(move.Name, Is.Not.EqualTo(ScrapingConstants.ExcludedRowHeaders.Miscellaneous), $"{nameof(character.Name)}");
                AssertMoveIsValid(move, character);
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void ScrapeThrowMovesForCharacter(WebCharacter character)
        {
            var throwMoveScraper = new ThrowMoveScraper(_scrapingServices);
            var throwMoves = throwMoveScraper.Scrape(character).ToList();

            CollectionAssert.IsNotEmpty(throwMoves);

            Assert.That(throwMoves.Count, Is.EqualTo(4)); //all chars have 4 throws.
            throwMoves.ForEach(move =>
            {
                Assert.That(move, Is.Not.Null, "move should not be null.");
                Assert.That(move.Name.Contains(MoveType.Throw.GetEnumDescription()), $"{move.Name}");
                Assert.That(move.MoveType, Is.EqualTo(MoveType.Throw.ToString().ToLower()), $"{move.Name}");
            });
        }

        [Test]
        public void ScrapeThrowMovesForCharacter_DonkeyKongHasEightThrows()
        {
            var throwMoveScraper = new ThrowMoveScraper(_scrapingServices);
            var throwMoves = throwMoveScraper.Scrape(Characters.DonkeyKong).ToList();

            CollectionAssert.IsNotEmpty(throwMoves);

            Assert.That(throwMoves.Count, Is.EqualTo(8)); //DK's cargo throw variants make his throw totals 8.
            throwMoves.ForEach(move =>
            {
                Assert.That(move, Is.Not.Null, "move should not be null.");
                Assert.That(move.Name.Contains(MoveType.Throw.GetEnumDescription()), $"{move.Name}");
                Assert.That(move.MoveType, Is.EqualTo(MoveType.Throw.ToString().ToLower()), $"{move.Name}");
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void ScrapeAerialMovesForCharacter(WebCharacter character)
        {
            var aerialMoveScrapingService = new AerialMoveScraper(_scrapingServices);

            var aerialMoves = aerialMoveScrapingService.Scrape(character).ToList();

            CollectionAssert.AllItemsAreNotNull(aerialMoves);
            CollectionAssert.AllItemsAreUnique(aerialMoves);
            CollectionAssert.IsNotEmpty(aerialMoves);

            aerialMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, character);
                Assert.That(move.LandingLag, Is.Not.Null);
                Assert.That(move.AutoCancel, Is.Not.Null);
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void ScrapeSpecialMovesForCharacter(WebCharacter character)
        {
            var specialMoveScrapingService = new SpecialMoveScraper(_scrapingServices);

            var specialMoves = specialMoveScrapingService.Scrape(character).ToList();

            CollectionAssert.AllItemsAreNotNull(specialMoves);
            CollectionAssert.AllItemsAreUnique(specialMoves);
            CollectionAssert.IsNotEmpty(specialMoves);

            specialMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, character);
            });
        }
    }
}
