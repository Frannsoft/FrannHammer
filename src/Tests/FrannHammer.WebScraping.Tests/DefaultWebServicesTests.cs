using System;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;
using Moq;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultWebServicesTests
    {
        [Test]
        public void DoesNotDownloadHtmlIfExistsInCache()
        {
            const string expectedTestUrl = "http://testUrl";

            var mockWebClientProvider = new Mock<IWebClientProvider>();

            var mockPageDownloader = new Mock<IPageDownloader>();
            mockPageDownloader.Setup(m => m.DownloadPageSource(It.IsAny<Uri>(), mockWebClientProvider.Object));

            var mockParserProvider = new Mock<IHtmlParserProvider>();

            var sut = new DefaultWebServices(mockParserProvider.Object, mockWebClientProvider.Object,
                mockPageDownloader.Object);

            sut.CreateParserFromSourceUrl(expectedTestUrl);
            sut.CreateParserFromSourceUrl(expectedTestUrl);

            mockPageDownloader.Verify(m => m.DownloadPageSource(It.IsAny<Uri>(), mockWebClientProvider.Object),
                Times.Once);
        }
    }
}
