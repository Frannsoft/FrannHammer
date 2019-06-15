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

                    var pikminTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

                    if (pikminTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows}'");
                    }

                    if (character.SourceUrl.Contains(Games.Ultimate.ToString()))
                    {
                        return pikminTableRows.SelectMany(
                            row => row.SelectNodes(ScrapingConstants.XPathMovementTableCellKeys),
                            (row, statName) => GetUniqueAttributeForUltimate(row, character)).Where(stat => stat != null);
                    }
                    else
                    {
                        return pikminTableRows.SelectMany(
                          row => row.SelectNodes($"{ScrapingConstants.XPathMovementTableCellKeys}[1]"),
                          (row, statName) => GetUniqueAttributeForSmash4(row, character)).Where(stat => stat != null);
                    }
                };
            }
        }

        private Pikmin GetUniqueAttributeForUltimate(HtmlNode row, WebCharacter character)
        {
            var uniqueData = GetUniqueAttributeCore(row, character);
            uniqueData.Game = Games.Ultimate;
            return uniqueData;
        }

        private Pikmin GetUniqueAttributeForSmash4(HtmlNode row, WebCharacter character)
        {
            var uniqueData = GetUniqueAttributeCore(row, character);
            uniqueData.Game = Games.Smash4;
            return uniqueData;
        }

        private Pikmin GetUniqueAttributeCore(HtmlNode row, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<Pikmin>();
            var pikminCells = row.SelectNodes("td");

            string name = pikminCells[0]?.InnerText;
            string attackThrow = CleanupValue(pikminCells[1]?.InnerText);
            string hp = CleanupValue(pikminCells[3]?.InnerText);

            int indexOfAttack = name.IndexOf("Attack");

            uniqueData.Name = $"{name.Substring(0, indexOfAttack).TrimEnd()}"; //just the stuff before attack
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;
            uniqueData.Attack = attackThrow.Substring(0, 4);
            uniqueData.Throw = attackThrow.Substring(5, 4);
            uniqueData.HP = hp;
            return uniqueData;
        }

        private static string CleanupValue(string dirtyValue) => dirtyValue.Replace(ScrapingConstants.EncodedValues.PercentSymbolEncoded, "%").Replace(ScrapingConstants.EncodedValues.TimesSymbolEncoded, "x");

    }
}
