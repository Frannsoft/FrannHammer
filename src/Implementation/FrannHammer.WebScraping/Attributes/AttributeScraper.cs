using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Attributes
{
    public abstract class AttributeScraper : IAttributeScraper
    {
        public abstract string AttributeName { get; }
        public virtual Func<WebCharacter, IEnumerable<ICharacterAttributeRow>> Scrape { get; }

        protected IAttributeScrapingServices ScrapingServices { get; }
        public string SourceUrl { get; set; }

        protected AttributeScraper(string baseUrl, IAttributeScrapingServices scrapingServices)
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));

            ScrapingServices = scrapingServices;

            string attributeName = string.Empty;
            if (AttributeName == "RunSpeed")
            {
                attributeName = "DashSpeed"; //grrr..
            }
            else
            {
                attributeName = AttributeName;
            }
            SourceUrl = $"{baseUrl}{attributeName}";

            Scrape = character =>
            {
                Guard.VerifyObjectNotNull(character, nameof(character));
                Guard.VerifyStringIsNotNullOrEmpty(character.Name, nameof(character.Name));

                var attributeValueRows = new List<ICharacterAttributeRow>();

                var htmlParser = ScrapingServices.CreateParserFromSourceUrl(SourceUrl);

                string attributeTableHtml =
                    htmlParser.GetSingle(ScrapingConstants.XPathTableNodeAttributesWithDescription) ??
                    htmlParser.GetSingle(ScrapingConstants.XPathTableNodeAttributesWithNoDescription);

                string xpath = ScrapingConstants.XPathEveryoneElseTableRow.Replace(ScrapingConstants.EveryoneOneElseAttributeKey, character.DisplayName);
                var tableHtmlNode = HtmlNode.CreateNode(attributeTableHtml);

                //scrape using default character name
                var attributeTableRows = tableHtmlNode?.SelectNodes(xpath);
                var rows = new List<HtmlNode>();

                if (attributeTableRows == null)
                {
                    //try all configured potential names for the character
                    foreach (string name in character.PotentialScrapingNames)
                    {
                        string altCharacterNameXPath =
                            ScrapingConstants.XPathEveryoneElseTableRow.Replace(
                                ScrapingConstants.EveryoneOneElseAttributeKey, name);
                        attributeTableRows = tableHtmlNode?.SelectNodes(altCharacterNameXPath);

                        if (attributeTableRows != null)
                        {
                            rows.AddRange(attributeTableRows.ToList());
                        }
                    }

                    //assume it's lumped in with everyone else value
                    if (attributeTableRows == null)
                    {
                        attributeTableRows =
                            tableHtmlNode?
                                .SelectNodes(ScrapingConstants.XPathEveryoneElseTableRow);

                        if (attributeTableRows != null)
                        {
                            rows.AddRange(attributeTableRows.ToList());
                        }
                    }

                    //try with modified 'everyone else' casing
                    if (attributeTableRows == null)
                    {
                        //GRRR CASING...'Everyone Else' vs 'Everyone else'.  I hate xpath.
                        attributeTableRows =
                            tableHtmlNode?.SelectNodes(
                                ScrapingConstants.XPathEveryoneElseTableRow.Replace(
                                    ScrapingConstants.EveryoneOneElseAttributeKey, "Everyone else"));

                        if (attributeTableRows == null)
                        {
                            //There is no data here.  Just return empty.  Sometimes that might be expected (like in Counters) so we shouldn't throw.
                            //throw new Exception(
                            //    $"Error getting attribute table data after attempting to scrape full table for character '{character.Name}' at url '{sourceUrl}'");
                            if (rows.Count == 0)
                            {
                                return attributeValueRows;
                            }
                        }
                        if (attributeTableRows != null)
                        {
                            rows.AddRange(attributeTableRows.ToList());
                        }
                    }
                }
                else
                {
                    rows.AddRange(attributeTableRows.ToList());
                }

                var headers = GetHeaders(htmlParser);

                foreach (var row in rows)
                {
                    var attributeValues = new List<IAttribute>();

                    var cells = row.SelectNodes(ScrapingConstants.XPathTableCells);

                    for (int i = 0; i < cells.Count; i++)
                    {
                        string headerValue = headers[i];
                        if (headerValue.Equals("character", StringComparison.OrdinalIgnoreCase) ||
                            headerValue.Equals("rank", StringComparison.OrdinalIgnoreCase))
                        { continue; }

                        var attributeValue = ScrapingServices.CreateAttribute();
                        attributeValue.Name = headerValue;
                        attributeValue.Value = cells[i].InnerText.Replace("&#215;", "x").Replace("&#37;", "%");
                        attributeValue.Owner = character.Name;
                        attributeValue.OwnerId = character.OwnerId;
                        attributeValues.Add(attributeValue);
                    }

                    var characterAttributeRow = ScrapingServices.CreateCharacterAttributeRow();
                    characterAttributeRow.Values = attributeValues;
                    characterAttributeRow.Name = AttributeName;
                    characterAttributeRow.Owner = character.Name;
                    characterAttributeRow.OwnerId = character.OwnerId;
                    characterAttributeRow.Game = SourceUrl.Contains("Ultimate") ? Games.Ultimate : Games.Smash4;
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
