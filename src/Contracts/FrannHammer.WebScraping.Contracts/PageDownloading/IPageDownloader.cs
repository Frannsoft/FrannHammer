using System;
using FrannHammer.WebScraping.Contracts.WebClients;

namespace FrannHammer.WebScraping.Contracts.PageDownloading
{
    public interface IPageDownloader
    {
        string DownloadPageSource(Uri requestedPageSourceUri, IWebClientProvider webClientProvider);
    }
}