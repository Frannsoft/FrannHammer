using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public abstract class DefaultMoveScrapingService
    {
        protected IMoveScrapingServices ScrapingServices { get; }
        protected string SourceUrl { get; }

        public abstract Func<IEnumerable<IMove>> Scrape { get; protected set; }

        protected DefaultMoveScrapingService(string sourceUrl, IMoveScrapingServices scrapingServices)
        {
            Guard.VerifyStringIsNotNullOrEmpty(sourceUrl, nameof(sourceUrl));
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));

            SourceUrl = sourceUrl;
            ScrapingServices = scrapingServices;
        }

        protected abstract IMove GetMove(HtmlNodeCollection cells);
        protected static HtmlNodeCollection GetTableCells(HtmlNode row) => row.SelectNodes(ScrapingXPathConstants.XPathTableCells);

        protected HtmlNodeCollection GetTableRows(string xpath)
        {
            //pull down source page html
            string pageHtml = ScrapingServices.DownloadPageSource(new Uri(SourceUrl));

            //get moves table html
            var movesTableHtml = ScrapingServices.CreateHtmlParser(pageHtml).GetSingle(xpath);

            var moveTableRows = HtmlNode.CreateNode(movesTableHtml)?.SelectNodes(ScrapingXPathConstants.XPathTableRows);

            if (moveTableRows == null)
            { throw new Exception($"Error getting move table data after attempting to scrape full table using xpath: '{ScrapingXPathConstants.XPathTableNodeGroundStats};"); }

            return moveTableRows;
        }

        protected static string GetStatName(HtmlNode cell)
        {
            var retVal = string.Empty;

            if (!string.IsNullOrEmpty(cell.InnerText))
            {
                retVal = cell.InnerText.Trim();
            }

            return retVal;
        }
    }
}
