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
    public class CommandSelectionScraper : IUniqueDataScraper
    {
        private const string CommandSelectionAttributeTableXPath = @"(//*/table[@id='AutoNumber3'])[2]";
        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;

        public CommandSelectionScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
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

                    string attributeTableHtml = htmlParser.GetSingle(CommandSelectionAttributeTableXPath);

                    var commandSelectionTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes("tbody/tr");

                    if (commandSelectionTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: 'tr'");
                    }

                    if (character.SourceUrl.Contains(Games.Ultimate.ToString()))
                    {
                        return commandSelectionTableRows.SelectMany(
                            row => row.SelectNodes(ScrapingConstants.XPathVegetableTableCellKeys),
                            (row, statName) => GetUniqueAttributeForUltimate(row, character)).Where(stat => stat != null);
                    }
                    else
                    {
                        return commandSelectionTableRows.SelectMany(
                          row => row.SelectNodes(ScrapingConstants.XPathVegetableTableCellKeys),
                          (row, statName) => GetUniqueAttributeForSmash4(row, character)).Where(stat => stat != null);
                    }
                };
            }
        }

        private CommandSelection GetUniqueAttributeForUltimate(HtmlNode row, WebCharacter character)
        {
            var uniqueData = GetUniqueAttributeCore(row, character);
            uniqueData.Game = Games.Ultimate;
            return uniqueData;
        }

        private CommandSelection GetUniqueAttributeForSmash4(HtmlNode row, WebCharacter character)
        {
            var uniqueData = GetUniqueAttributeCore(row, character);
            uniqueData.Game = Games.Smash4;
            return uniqueData;
        }

        private CommandSelection GetUniqueAttributeCore(HtmlNode row, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<CommandSelection>();
            var commandSelectionCells = row.SelectNodes("td");

            string name = row.SelectSingleNode("th")?.InnerText;
            string description = commandSelectionCells[0]?.InnerText;

            uniqueData.Game = Games.Smash4;
            uniqueData.Name = $"CommandSelection - {name}";
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;
            uniqueData.Description = CleanupValue(description);
            return uniqueData;
        }

        private static string CleanupValue(string dirtyValue) => dirtyValue.Replace(ScrapingConstants.EncodedValues.PercentSymbolEncoded, "%");

    }
}
