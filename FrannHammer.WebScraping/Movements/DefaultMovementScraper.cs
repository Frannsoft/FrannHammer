using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Movements
{
    public class DefaultMovementScraper : IMovementScraper
    {
        private readonly IMovementScrapingServices _scrapingServices;

        public DefaultMovementScraper(IMovementScrapingServices scrapingServices)
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));
            _scrapingServices = scrapingServices;
        }

        public IEnumerable<IMovement> GetMovementsForCharacter(WebCharacter character)
        {
            var htmlParser = _scrapingServices.CreateParserFromSourceUrl(character.SourceUrl);

            //get movement table html
            string movementTableHtml = htmlParser.GetSingle(ScrapingConstants.XPathTableNodeMovementStats);

            var movementTableRows = HtmlNode.CreateNode(movementTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

            if (movementTableRows == null)
            { throw new Exception($"Error getting movement table data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows};"); }

            return movementTableRows.SelectMany(
                row => row.SelectNodes(ScrapingConstants.XPathMovementTableCellKeys),
                (row, statName) => GetMovement(statName)).Where(stat => stat != null);
        }

        private IMovement GetMovement(HtmlNode nameCell)
        {
            var movement = default(IMovement);

            var rawNameCellText = nameCell.InnerText;
            if (!string.IsNullOrEmpty(rawNameCellText))
            {
                var name = GetStatName(nameCell);
                var valueCell = nameCell.SelectSingleNode(ScrapingConstants.XPathTableCellValues);

                var rawValueText = valueCell.InnerText;
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

                movement = _scrapingServices.CreateMovement();
                movement.Name = name;
                movement.Value = value;
            }

            return movement;
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
