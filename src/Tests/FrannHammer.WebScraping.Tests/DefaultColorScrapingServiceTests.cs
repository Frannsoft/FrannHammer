using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultColorScrapingServiceTests
    {
        [Test]
        [TestCase("wario", "#FECB4C")]
        [TestCase("ken", "#cdd7dc")]
        public async Task PullsExpectedColorForCharacter(string characterName, string expectedColor)
        {
            string css = new DefaultPageDownloader()
                .DownloadPageSource(new Uri("http://kuroganehammer.com/css/character.css"),
                new DefaultWebClientProvider());

            var sut = new DefaultColorScrapingService(css);
            string actualColor = await sut.GetColorHexValue(characterName);

            Assert.That(actualColor.ToLower(), Is.EqualTo(expectedColor.ToLower()));
        }
    }
}
