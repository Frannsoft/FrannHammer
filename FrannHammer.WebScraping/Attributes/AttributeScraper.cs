﻿using System;
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
        public virtual Func<string, IEnumerable<ICharacterAttributeRow>> Scrape { get; }

        protected AttributeScraper(string sourceUrl, IAttributeScrapingServices scrapingServices)
        {
            Guard.VerifyStringIsNotNullOrEmpty(sourceUrl, nameof(sourceUrl));
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));

            Scrape = characterName =>
            {
                Guard.VerifyStringIsNotNullOrEmpty(characterName, nameof(characterName));
                var attributeValueRows = new List<ICharacterAttributeRow>();
                var htmlParser = scrapingServices.CreateParserFromSourceUrl(sourceUrl);

                string attributeTableHtml =
                    htmlParser.GetSingle(ScrapingConstants.XPathTableNodeAttributesWithDescription) ??
                    htmlParser.GetSingle(ScrapingConstants.XPathTableNodeAttributesWithNoDescription);

                string xpath = ScrapingConstants.XPathEveryoneElseTableRow.Replace(ScrapingConstants.EveryoneOneElseAttributeKey, characterName);
                var attributeTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(xpath);

                if (attributeTableRows == null)
                {
                    attributeTableRows =
                        HtmlNode.CreateNode(attributeTableHtml)?
                            .SelectNodes(ScrapingConstants.XPathEveryoneElseTableRow);

                    if (attributeTableRows == null)
                    {
                        //GRRR CASING...'Everyone Else' vs 'Everyone else'.  I hate xpath.
                        attributeTableRows =
                       HtmlNode.CreateNode(attributeTableHtml)?
                           .SelectNodes(ScrapingConstants.XPathEveryoneElseTableRow.Replace(ScrapingConstants.EveryoneOneElseAttributeKey, "Everyone else"));

                        if (attributeTableRows == null)
                        {
                            throw new Exception(
                                "Error getting attribute table data after attempting to scrape full table");
                        }
                    }
                }

                var headers = GetHeaders(htmlParser);

                foreach (var row in attributeTableRows)
                {
                    var attributeValues = new List<IAttribute>();

                    var cells = row.SelectNodes(ScrapingConstants.XPathTableCells);

                    for (int i = 0; i < cells.Count; i++)
                    {
                        var attributeValue = scrapingServices.CreateAttribute();
                        attributeValue.Name = headers[i];
                        attributeValue.Value = cells[i].InnerText;
                        attributeValue.Owner = characterName;
                        attributeValues.Add(attributeValue);
                    }

                    var characterAttributeRow = new DefaultCharacterAttributeRow(attributeValues)
                    {
                        Name = AttributeName
                    };
                    attributeValueRows.Add(characterAttributeRow);
                }

                return attributeValueRows;
            };
        }

        protected static IList<string> GetHeaders(IHtmlParser htmlParser)
        {
            var rawHeaders = htmlParser.GetCollection(ScrapingConstants.XPathTableNodeAttributeHeaders) ??
                             htmlParser.GetCollection(ScrapingConstants.XPathTableNodeAttributeHeadersWithNoDescription);
            var headers = (from row in rawHeaders
                           from cell in HtmlNode.CreateNode(row).SelectNodes(ScrapingConstants.XPathTableCells)
                           select cell.InnerText).ToList();

            return headers;
        }
    }
}
