using System;

namespace FrannHammer.WebScraping
{
    public interface IPageDownloader
    {
        string DownloadPageSource(Uri requestedPageSourceUri, IWebClientProvider webClientProvider);
    }
}