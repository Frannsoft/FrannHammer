using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public class LimitBreakScraper : IUniqueDataScraper
    {
        private const string LimitBreakAttributeTableXPath = @"(//*/table[@id='AutoNumber1'])[2]";

        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;

        public LimitBreakScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
        {
            Guard.VerifyObjectNotNull(uniqueDataScrapingServices, nameof(uniqueDataScrapingServices));

            _uniqueDataScrapingServices = uniqueDataScrapingServices;
        }

        public Func<WebCharacter, IEnumerable<IUniqueData>> Scrape
        {
            get
            {
                return character =>
                {
                    Guard.VerifyObjectNotNull(character, nameof(character));
                    Guard.VerifyStringIsNotNullOrEmpty(character.Name, character.Name);

                    var htmlParser = _uniqueDataScrapingServices.CreateParserFromSourceUrl(character.SourceUrl);

                    string attributeTableHtml = htmlParser.GetSingle(LimitBreakAttributeTableXPath);

                    var limitBreakTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

                    if (limitBreakTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows}'");
                    }

                    return limitBreakTableRows.SelectMany(
                        row => row.SelectNodes(ScrapingConstants.XPathMovementTableCellKeys),
                        (row, statName) => GetUniqueAttribute(statName, character)).Where(stat => stat != null);
                };
            }
        }

        private IUniqueData GetUniqueAttribute(HtmlNode nameCell, WebCharacter character)
        {
            var uniqueData = default(IUniqueData);

            var rawNameCellText = nameCell.InnerText;
            if (!string.IsNullOrEmpty(rawNameCellText))
            {
                string name = GetStatName(nameCell);
                var valueCell = nameCell.SelectSingleNode(ScrapingConstants.XPathTableCellValues);

                string rawValueText = valueCell.InnerText;
                string value;

                if (rawValueText.Contains("["))
                {
                    var checkRank = valueCell.InnerText.Split('[');
                    value = checkRank[0];
                }
                else
                {
                    value = rawValueText;
                }

                uniqueData = _uniqueDataScrapingServices.Create();
                uniqueData.Name = name;
                uniqueData.Value = value;
                uniqueData.Owner = character.Name;
                uniqueData.OwnerId = character.OwnerId;
            }

            return uniqueData;
        }

        private static string GetStatName(HtmlNode cell)
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
