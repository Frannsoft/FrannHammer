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
    public class VegetableScraper : IUniqueDataScraper
    {
        private const string VegetableAttributeTableXPath = @"(//*/table[@id='AutoNumber3'])[2]";
        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;

        public VegetableScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
        {
            Guard.VerifyObjectNotNull(uniqueDataScrapingServices, nameof(uniqueDataScrapingServices));

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

                    string attributeTableHtml = htmlParser.GetSingle(VegetableAttributeTableXPath);

                    var vegetableTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes("tr");

                    if (vegetableTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: 'tr'");
                    }

                    if (character.SourceUrl.Contains(Games.Ultimate.ToString()))
                    {
                        return vegetableTableRows.SelectMany(
                            row => row.SelectNodes(ScrapingConstants.XPathVegetableTableCellKeys),
                            (row, statName) => GetUniqueAttributeForUltimate(row, character)).Where(stat => stat != null);
                    }
                    else
                    {
                        return vegetableTableRows.SelectMany(
                          row => row.SelectNodes(ScrapingConstants.XPathVegetableTableCellKeys),
                          (row, statName) => GetUniqueAttributeForSmash4(row, character)).Where(stat => stat != null);
                    }
                };
            }
        }

        private Vegetable GetUniqueAttributeForUltimate(HtmlNode row, WebCharacter character)
        {
            var uniqueData = GetUniqueAttributeCore(row, character);
            uniqueData.Game = Games.Ultimate;
            return uniqueData;
        }

        private Vegetable GetUniqueAttributeForSmash4(HtmlNode row, WebCharacter character)
        {
            var uniqueData = GetUniqueAttributeCore(row, character);
            uniqueData.Game = Games.Smash4;
            return uniqueData;
        }

        private Vegetable GetUniqueAttributeCore(HtmlNode row, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<Vegetable>();
            var vegetableCells = row.SelectNodes("td");

            string name = row.SelectSingleNode("th")?.InnerText;
            string chance = vegetableCells[0]?.InnerText;
            string damageDealt = vegetableCells[1]?.InnerText;

            uniqueData.Game = Games.Smash4;
            uniqueData.Name = $"Vegetable - {name}";
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;
            uniqueData.Chance = CleanupValue(chance);
            uniqueData.DamageDealt = CleanupValue(damageDealt);
            return uniqueData;
        }

        private static string CleanupValue(string dirtyValue) => dirtyValue.Replace(ScrapingConstants.EncodedValues.PercentSymbolEncoded, "%");

    }
}
