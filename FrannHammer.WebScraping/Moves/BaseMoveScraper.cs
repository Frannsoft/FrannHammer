using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Moves;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Moves
{
    public abstract class BaseMoveScraper : IMoveScraper
    {
        protected IMoveScrapingServices ScrapingServices { get; }

        public abstract Func<string, IEnumerable<IMove>> Scrape { get; protected set; }

        protected BaseMoveScraper(IMoveScrapingServices scrapingServices)
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));
            ScrapingServices = scrapingServices;
        }

        protected abstract IMove GetMove(HtmlNodeCollection cells);

        protected static HtmlNodeCollection GetTableCells(HtmlNode row) => row.SelectNodes(ScrapingXPathConstants.XPathTableCells);

        protected HtmlNodeCollection GetTableRows(string sourceUrl, string xpath)
        {
            var htmlParser = ScrapingServices.CreateParserFromSourceUrl(sourceUrl);

            //get moves table html
            var movesTableHtml = htmlParser.GetSingle(xpath);

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
