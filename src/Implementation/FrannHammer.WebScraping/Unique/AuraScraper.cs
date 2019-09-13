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
    public class AuraScraper : IUniqueDataScraper
    {
        private readonly IUniqueDataScrapingServices _uniqueDataScrapingServices;
        private const string AuraAttributeTableXPath = @"(//*/table[@id='AutoNumber1'])[2]";

        public AuraScraper(IUniqueDataScrapingServices uniqueDataScrapingServices)
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

                    string attributeTableHtml = htmlParser.GetSingle(AuraAttributeTableXPath);

                    var auraTableRows = HtmlNode.CreateNode(attributeTableHtml)?.SelectNodes(ScrapingConstants.XPathTableRows);

                    if (auraTableRows == null)
                    {
                        throw new Exception(
                            $"Error getting unique data after attempting to scrape full table using xpath: '{ScrapingConstants.XPathTableRows}'");
                    }

                    var uniqueData = new List<Aura>();
                    if (character.SourceUrl.Contains(Games.Ultimate.ToString()))
                    {
                        var aura = GetUniqueAttributeForSmash4(auraTableRows, character); //cloud doesn't exist for Ultimate kh data yet
                        uniqueData.Add(aura);
                    }
                    else
                    {
                        var aura = GetUniqueAttributeForSmash4(auraTableRows, character);
                        uniqueData.Add(aura);
                    }
                    return uniqueData;
                };
            }


        }

        private Aura GetUniqueAttributeForSmash4(HtmlNodeCollection rows, WebCharacter character)
        {
            var uniqueData = _uniqueDataScrapingServices.Create<Aura>();
            uniqueData.Owner = character.Name;
            uniqueData.OwnerId = character.OwnerId;
            uniqueData.MinPercentAuraMultiplier = GetValue("Min % Aura &#215;", rows);
            uniqueData.MaxPercentAuraMultiplier = GetValue("Max % Aura &#215;", rows);
            uniqueData.AuraBaselinePercent = GetValue("Aura Baseline %", rows);
            uniqueData.AuraCeilingPercent = GetValue("Aura Ceiling %", rows);

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

        private static string CleanupValue(string dirtyValue) => dirtyValue.Replace(ScrapingConstants.EncodedValues.PercentSymbolEncoded, "%").Replace(ScrapingConstants.EncodedValues.TimesSymbolEncoded, "x");

    }
}
