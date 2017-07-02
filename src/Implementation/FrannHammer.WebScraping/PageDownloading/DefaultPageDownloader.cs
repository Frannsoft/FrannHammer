using System;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;

namespace FrannHammer.WebScraping.PageDownloading
{
    public class DefaultPageDownloader : IPageDownloader
    {
        public string DownloadPageSource(Uri requestedPageSourceUri, IWebClientProvider webClientProvider)
        {
            Guard.VerifyObjectNotNull(webClientProvider, nameof(webClientProvider));

            if (!requestedPageSourceUri.IsWellFormedOriginalString())
            { throw new ArgumentException($"'{requestedPageSourceUri}' is not well-formed.  Additional escaping may be required"); }

            string pageSource;

            using (var client = webClientProvider.Create())
            {
                pageSource = client.DownloadString(requestedPageSourceUri);
            }

            return pageSource;
        }
    }
}
