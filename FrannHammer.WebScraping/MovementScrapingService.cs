using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public class MovementScrapingService : IMovementScrapingService
    {
        private readonly IHtmlParser _htmlParser;
        private readonly IMovementProvider _movementProvider;

        public MovementScrapingService(IHtmlParser htmlParser, IMovementProvider movementProvider)
        {
            Guard.VerifyObjectNotNull(htmlParser, nameof(htmlParser));
            Guard.VerifyObjectNotNull(movementProvider, nameof(movementProvider));

            _htmlParser = htmlParser;
            _movementProvider = movementProvider;
        }

        public IEnumerable<IMovement> GetMovements(string xpath)
        {
            //get movement table html
            string movementTableHtml = _htmlParser.GetSingle(xpath);

            var movementTableRows = HtmlNode.CreateNode(movementTableHtml)?.SelectNodes(ScrapingXPathConstants.XPathTableRows);

            if (movementTableRows == null)
            { throw new Exception($"Error getting movement table data after attempting to scrape full table using xpath: '{xpath};"); }

            return movementTableRows.SelectMany(
                row => row.SelectNodes(ScrapingXPathConstants.XPathMovementTableCellKeys),
                (row, statName) => GetMovement(statName)).Where(stat => stat != null);
        }

        private IMovement GetMovement(HtmlNode nameCell)
        {
            var movement = default(IMovement);

            var rawNameCellText = nameCell.InnerText;
            if (!string.IsNullOrEmpty(rawNameCellText))
            {
                var name = GetStatName(nameCell);
                var valueCell = nameCell.SelectSingleNode(ScrapingXPathConstants.XPathTableCellValues);

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

                movement = _movementProvider.Create();
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
