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
        public void GroundMoveScraperShouldExcludeThrowMoves()
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

            groundMoves.ForEach(move =>
            {
                Assert.That(!move.Name.Contains(MoveType.Throw.GetEnumDescription()),
                    $"{nameof(IMove.Name)} should not contain {MoveType.Throw.GetEnumDescription()}.  This means the scraper might be pulling in throw moves.");
            });
        }

        [Test]
        public void ScrapeThrowMovesForCharacter()
        {
            var throwMoveScraper = new ThrowMoveScraper(_scrapingServices);

            var throwMoves = throwMoveScraper.Scrape(Characters.Greninja).ToList();

            CollectionAssert.IsNotEmpty(throwMoves);

            throwMoves.ForEach(move =>
            {
                Assert.That(move, Is.Not.Null, "move should not be null.");
                Assert.That(move.Name.Contains(MoveType.Throw.GetEnumDescription()) || move.Name.ToLower().Contains("grab"),
                    $"{move.Name} should contain {MoveType.Throw.GetEnumDescription()} or 'grab'.  This means the scraper might NOT be pulling in throw moves.");
            });
        }

        [Test]
        public void ScrapeAerialMovesForCharacter()
        {
            var aerialMoveScrapingService = new AerialMoveScraper(_scrapingServices);

            var aerialMoves = aerialMoveScrapingService.Scrape(Characters.Greninja).ToList();

            CollectionAssert.AllItemsAreNotNull(aerialMoves);
            CollectionAssert.AllItemsAreUnique(aerialMoves);
            CollectionAssert.IsNotEmpty(aerialMoves);

            aerialMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, Characters.Greninja);
                Assert.That(move.LandingLag, Is.Not.Null);
                Assert.That(move.AutoCancel, Is.Not.Null);
            });
        }

        [Test]
        public void ScrapeSpecialMovesForCharacter()
        {
            var specialMoveScrapingService = new SpecialMoveScraper(_scrapingServices);

            var specialMoves = specialMoveScrapingService.Scrape(Characters.Greninja).ToList();

            CollectionAssert.AllItemsAreNotNull(specialMoves);
            CollectionAssert.AllItemsAreUnique(specialMoves);
            CollectionAssert.IsNotEmpty(specialMoves);

            specialMoves.ForEach(move =>
            {
                AssertMoveIsValid(move, Characters.Greninja);
            });
        }
    }
}
