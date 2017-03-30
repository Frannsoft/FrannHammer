using System;
using FrannHammer.Utility;

namespace FrannHammer.WebScraping
{
    public class PageDownloader : IPageDownloader
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
