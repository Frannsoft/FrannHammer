using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Unique
{
    public class PikminScraper : IUniqueDataScraper
    {
        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;
        private const string PikminAttributeTableXPath = @"(//*/table[@id='AutoNumber1'])[2]";

        public PikminScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
        {
            _uniqueDataScrapingServices = uniqueDataScrapingServices;
        }

        public Func<WebCharacter, IEnumerable<object>> Scrape
        {
            get
            {
                return character =>
                {
                    Guard.VerifyObjectNotNull(character, nameof(character));
                    Guard.VerifyStringIsNotNullOrEmpty(character.Name, character.Name);

                    var htmlParser = _uniqueDataScrapingServices.CreateParserFromSourceUrl(character.SourceUrl);

                    string attributeTableHtml = htmlParser.GetSingle(PikminAttributeTableXPath);

                    var floatTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

                    if (floatTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows}'");
                    }

                    var uniqueData = new List<Float>();
                    var floatData = GetUniqueAttribute(floatTableRows, character);
                    floatData.Game = character.SourceUrl.Contains("Ultimate") ? Games.Ultimate : Games.Smash4;
                    uniqueData.Add(floatData);
                    return uniqueData;
                };
            }
        }

        private Float GetUniqueAttribute(HtmlNodeCollection rows, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<Float>();
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;

            uniqueData.DurationInFrames = GetValue("Duration (Frames)", rows);
            uniqueData.DurationInSeconds = GetValue("Duration (Seconds)", rows);
            return uniqueData;
        }

        private static string GetValue(string propertyName, HtmlNodeCollection rows)
        {
            var cellContainingMatch = rows.FirstOrDefault(node =>
            {
                var row = node.SelectSingleNode($@"td[text()='{propertyName}']");
                return row != null;

            })?.SelectSingleNode($"td[text()='{propertyName}']");

            string value = cellContainingMatch?.NextSibling?.NextSibling.InnerText;
            return value;
        }
    }
}
