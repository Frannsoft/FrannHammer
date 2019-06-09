using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain;
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

                    var uniqueData = new List<LimitBreak>();
                    if (character.SourceUrl.Contains(Games.Ultimate.ToString()))
                    {
                        var limitBreak = GetUniqueAttributeForSmash4(limitBreakTableRows, character); //cloud doesn't exist for Ultimate kh data yet
                        uniqueData.Add(limitBreak);
                    }
                    else
                    {
                        var limitBreak = GetUniqueAttributeForSmash4(limitBreakTableRows, character);
                        uniqueData.Add(limitBreak);
                    }
                    return uniqueData;
                };
            }


        }

        private LimitBreak GetUniqueAttributeForSmash4(HtmlNodeCollection rows, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<LimitBreak>();
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;

            uniqueData.FramesToCharge = GetValue("Frames to Charge", rows);
            uniqueData.PercentDealtToCharge = GetValue("% Dealt to Charge", rows);
            uniqueData.RunSpeed = GetValue("Run Speed", rows);
            uniqueData.WalkSpeed = GetValue("Walk Speed", rows);
            uniqueData.Gravity = GetValue("Gravity", rows);
            uniqueData.SHAirTime = GetValue("SH Air Time", rows);
            uniqueData.GainedPerFrame = GetValue("Gained per Frame", rows);
            uniqueData.PercentTakenToCharge = GetValue("% Taken to Charge", rows);
            uniqueData.AirSpeed = GetValue("Air Speed", rows);
            uniqueData.FallSpeed = GetValue("Fall Speed", rows);
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
