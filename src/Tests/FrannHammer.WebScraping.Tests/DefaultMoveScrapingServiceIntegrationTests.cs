using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    [TestFixtureSource(nameof(FixtureArgs))]
    public class DefaultMoveScrapingServiceIntegrationTests
    {
        private static IMoveScrapingServices _scrapingServices = MakeDefaultMoveScrapingServices();

        private readonly string _game;

        public DefaultMoveScrapingServiceIntegrationTests(string game)
        {
            _game = $"{game}/";
        }

        private static object[] FixtureArgs =
        {
            new object[]{"Smash4"},
            new object[]{"Ultimate"}
        };

        public static DefaultMoveScrapingServices MakeDefaultMoveScrapingServices()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var moveProvider = new DefaultMoveProvider(instanceIdGenerator);
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);

            return new DefaultMoveScrapingServices(moveProvider, webServices);
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
            yield return Characters.Ryu;
            yield return Characters.Bowser;
        }

        [Test]
        public void ScrapeGroundMoves_WhenMoveHas_GroundAirOnlyHitboxes()
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);

            var character = Characters.Fox;
            character.Game = Games.Ultimate;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Ultimate/{character.EscapedCharacterName}";

            var groundMoves = groundMoveScrapingService.Scrape(character).ToList();

            var utilt = groundMoves.First(m => m.Name == "Utilt");

            Assert.That(utilt.BaseDamage, Is.EqualTo("6/6/8/7|1v1: 7.2/7.2/9.6/8.4"));
        }

        [Test]
        public void ScrapeGroundMoves_HeaderRowsAreExcludeFromScrapedValues()
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);

            var character = Characters.Ryu;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
            var groundMoves = groundMoveScrapingService.Scrape(character).ToList();

            CollectionAssert.AllItemsAreNotNull(groundMoves);
            CollectionAssert.AllItemsAreUnique(groundMoves);
            CollectionAssert.IsNotEmpty(groundMoves);

            groundMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, character);
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

            var character = Characters.Ryu;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
            var groundMoves = groundMoveScrapingService.Scrape(character).ToList();

            CollectionAssert.AllItemsAreNotNull(groundMoves);
            CollectionAssert.AllItemsAreUnique(groundMoves);
            CollectionAssert.IsNotEmpty(groundMoves);

            groundMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, character);
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void GroundMoveScraperShouldExcludeThrowMoves(WebCharacter character)
        {
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
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
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
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

            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
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

            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
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
        public void ThrowMoveScraperExtractsCorrectData_Smash4()
        {
            var throwMoveScraper = new ThrowMoveScraper(_scrapingServices);

            var character = new Ganondorf();
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}{character.EscapedCharacterName}";
            var throwMoves = throwMoveScraper.Scrape(character).ToList();

            CollectionAssert.IsNotEmpty(throwMoves);

            Assert.That(throwMoves.Count, Is.EqualTo(4));
            var backThrow = throwMoves.SingleOrDefault(t => t.Name == "Bthrow");

            Assert.That(backThrow.IsWeightDependent, Is.EqualTo(false));
            Assert.That(backThrow.BaseDamage, Is.EqualTo("5, 5"));
            Assert.That(backThrow.Angle, Is.EqualTo("43"));
            Assert.That(backThrow.BaseKnockBackSetKnockback, Is.EqualTo("30"));
            Assert.That(backThrow.KnockbackGrowth, Is.EqualTo("130"));
        }

        [Test]
        public void ThrowMoveScraperExtractsCorrectData_Ultimate()
        {
            var throwMoveScraper = new ThrowMoveScraper(_scrapingServices);

            var character = new Ganondorf();
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{Keys.UltimateUrl}{character.EscapedCharacterName}";
            var throwMoves = throwMoveScraper.Scrape(character).ToList();

            CollectionAssert.IsNotEmpty(throwMoves);

            Assert.That(throwMoves.Count, Is.EqualTo(4));
            var backThrow = throwMoves.SingleOrDefault(t => t.Name == "Bthrow");

            Assert.That(backThrow.IsWeightDependent, Is.EqualTo(false));
            Assert.That(backThrow.BaseDamage, Is.EqualTo("5, 5;1v1: 6, 6"));
            Assert.That(backThrow.Angle, Is.EqualTo("43"));
            Assert.That(backThrow.BaseKnockBackSetKnockback, Is.EqualTo("30"));
            Assert.That(backThrow.KnockbackGrowth, Is.EqualTo("130"));
        }

        [Test]
        public void ScrapeThrowMovesForCharacter_DonkeyKongHasEightThrows()
        {
            var throwMoveScraper = new ThrowMoveScraper(_scrapingServices);

            var character = Characters.DonkeyKong;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
            var throwMoves = throwMoveScraper.Scrape(character).ToList();

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
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
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
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{_game}{character.EscapedCharacterName}";
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
