using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.Unique;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class UniqueDataScrapersTests
    {
        [Test]
        public void ScrapeUniqueData_CloudLimitBreak_ReturnsLimitBreakTableData()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);
            var uniqueDataProvider = new DefaultUniqueDataProvider(instanceIdGenerator);
            var scrapingServices = new DefaultUniqueDataScrapingServices(uniqueDataProvider, webServices);

            var limitBreakScraper = new LimitBreakScraper(scrapingServices);

            var character = Characters.Cloud;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Smash4/{character.EscapedCharacterName}";

            var limitBreakResultData = limitBreakScraper.Scrape(character).ToList();

            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Frames to Charge");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "% Dealt to Charge");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Run Speed");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Walk Speed");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Gravity");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "SH Air Time");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Gained per Frame");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "% Taken to Charge");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Air Speed");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Fall Speed");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "Air Acceleration");
            AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(limitBreakResultData, "FH Air Time");
        }

        private static void AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(IEnumerable<IUniqueData> uniqueDataList, string keyUnderTest)
        {
            var uniqueData = uniqueDataList.FirstOrDefault(u => u.Name.Equals(keyUnderTest, StringComparison.OrdinalIgnoreCase));

            Assert.That(uniqueData, Is.Not.Null, $"does not contain item with {nameof(IUniqueData.Name)} '{keyUnderTest}'");

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(uniqueData.Value, Is.Not.Empty);
        }
    }
}
