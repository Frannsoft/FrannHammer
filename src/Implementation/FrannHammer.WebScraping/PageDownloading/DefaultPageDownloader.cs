using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;
using System;
using System.Net;

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

            try
            {
                using (var client = webClientProvider.Create())
                {
                    pageSource = client.DownloadString(requestedPageSourceUri);
                }
            }
            catch (WebException ex) when (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
            {
                throw new PageNotFoundException(ex.Message, ex);
            }

            return pageSource;
        }
    }
}
