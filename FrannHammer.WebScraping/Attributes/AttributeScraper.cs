using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Attributes
{
    public abstract class AttributeScraper : IAttributeScraper
    {
        public abstract string AttributeName { get; }
        public virtual Func<IEnumerable<ICharacterAttributeRow>> Scrape { get; }

        protected AttributeScraper(string sourceUrl, IAttributeScrapingServices scrapingServices)
        {
            Guard.VerifyStringIsNotNullOrEmpty(sourceUrl, nameof(sourceUrl));
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));

            Scrape = () =>
            {
                var attributeValueRows = new List<ICharacterAttributeRow>();
                var htmlParser = scrapingServices.CreateParserFromSourceUrl(sourceUrl);

                string attributeTableHtml =
                    htmlParser.GetSingle(ScrapingXPathConstants.XPathTableNodeAttributesWithDescription) ??
                    htmlParser.GetSingle(ScrapingXPathConstants.XPathTableNodeAttributesWithNoDescription);

                var attributeTableRows =
                    HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingXPathConstants.XPathTableRows);

                if (attributeTableRows == null)
                {
                    throw new Exception("Error getting attribute table data after attempting to scrape full table");
                }

                var headers = GetHeaders(htmlParser);

                foreach (var row in attributeTableRows)
                {
                    var attributeValues = new List<IAttribute>();

                    var cells = row.SelectNodes(ScrapingXPathConstants.XPathTableCells);

                    for (int i = 0; i < cells.Count; i++)
                    {
                        var attributeValue = scrapingServices.CreateAttribute();
                        attributeValue.Name = headers[i];
                        attributeValue.Value = cells[i].InnerText;
                        attributeValue.AttributeFlag = AttributeName;
                        attributeValues.Add(attributeValue);
                    }

                    attributeValueRows.Add(new DefaultCharacterAttributeRow(attributeValues));
                }

                return attributeValueRows;
            };
        }

        private static IList<string> GetHeaders(IHtmlParser htmlParser)
        {
            var rawHeaders = htmlParser.GetCollection(ScrapingXPathConstants.XPathTableNodeAttributeHeaders) ??
                             htmlParser.GetCollection(ScrapingXPathConstants.XPathTableNodeAttributeHeadersWithNoDescription);
            var headers = (from row in rawHeaders
                           from cell in HtmlNode.CreateNode(row).SelectNodes(ScrapingXPathConstants.XPathTableCells)
                           select cell.InnerText).ToList();

            return headers;
        }
    }
}
