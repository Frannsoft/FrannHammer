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
    public class OneWingedAngelScraper : IUniqueDataScraper
    {
        private const string LimitBreakAttributeTableXPath = @"(//*/table[@id='AutoNumber1'])[2]";

        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;

        public OneWingedAngelScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
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

                    string attributeTableHtml = htmlParser.GetSingle(LimitBreakAttributeTableXPath);

                    var limitBreakTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

                    if (limitBreakTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows}'");
                    }

                    var uniqueData = new List<OneWingedAngel>();
                    var limitBreak = GetUniqueAttribute(limitBreakTableRows, character); //cloud doesn't exist for Ultimate kh data yet
                    limitBreak.Game = Games.Ultimate;
                    uniqueData.Add(limitBreak);
                    return uniqueData;
                };
            }
        }

        private OneWingedAngel GetUniqueAttribute(HtmlNodeCollection rows, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<OneWingedAngel>();
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;

            uniqueData.RunAcceleration = GetValue("Run Accel", rows);
            uniqueData.WalkAcceleration = GetValue("Walk Accel", rows);
            uniqueData.RunSpeed = GetValue("Run Speed", rows);
            uniqueData.WalkSpeed = GetValue("Walk Speed", rows);
            uniqueData.Gravity = GetValue("Gravity", rows);
            uniqueData.SHAirTime = GetValue("SH Air Time", rows);
            uniqueData.DamageDealt = GetValue("Damage Dealt", rows);
            uniqueData.MaxJumps = GetValue("Max Jumps", rows);
            uniqueData.AirSpeed = GetValue("Air Speed", rows);
            uniqueData.AirSpeedFriction = GetValue("Air Speed Friction", rows);
            uniqueData.FallSpeed = GetValue("Fall Speed", rows);
            uniqueData.FastFallSpeed = GetValue("Fast Fall Speed", rows);
            uniqueData.AirAcceleration = GetValue("Air Acceleration", rows);
            uniqueData.FHAirTime = GetValue("FH Air Time", rows);

            return uniqueData;
        }

        private static string GetValue(string propertyName, HtmlNodeCollection rows)
        {
            string matchingCellXPath = $@"td[text()='{propertyName}']";

            var cellContainingMatch = rows.FirstOrDefault(node =>
            {
                var row = node.SelectSingleNode(matchingCellXPath);
                return row != null;

            })?.SelectSingleNode(matchingCellXPath);

            string value = cellContainingMatch?.NextSibling?.NextSibling.InnerText;

            return CleanupValue(value);
        }

        private static string CleanupValue(string dirtyValue) => dirtyValue.Replace(ScrapingConstants.EncodedValues.PercentSymbolEncoded, "%");
    }
}
