using System;

namespace FrannHammer.WebScraping.PageDownloading
{
    public class PageNotFoundException : Exception
    {
        public PageNotFoundException(string message) : base(message)
        {
        }

        public PageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
