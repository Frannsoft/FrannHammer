using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping
{
    public class DefaultMovementScrapingService : IMovementScrapingService
    {
        private readonly IMovementScrapingServices _scrapingServices;

        public DefaultMovementScrapingService(IMovementScrapingServices scrapingServices)
        {
            Guard.VerifyObjectNotNull(scrapingServices, nameof(scrapingServices));
            _scrapingServices = scrapingServices;
        }

        public IEnumerable<IMovement> GetMovementsForCharacter(WebCharacter character)
        {
            //pull down source page html
            string pageHtml = _scrapingServices.DownloadPageSource(new Uri(character.SourceUrl));

            //get movement table html
            string movementTableHtml = _scrapingServices.CreateHtmlParser(pageHtml).GetSingle(ScrapingXPathConstants.XPathTableNodeMovementStats);

            var movementTableRows = HtmlNode.CreateNode(movementTableHtml)?.SelectNodes(ScrapingXPathConstants.XPathTableRows);

            if (movementTableRows == null)
            { throw new Exception($"Error getting movement table data after attempting to scrape full table using xpath: '{ScrapingXPathConstants.XPathTableRows};"); }

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
