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
        [TestCase("Smash4")]
        public void ScrapeUniqueData_CloudLimitBreak_ReturnsLimitBreakTableData(string gameUrlModifier)
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
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}{gameUrlModifier}/{character.EscapedCharacterName}";

            var limitBreakResultData = limitBreakScraper.Scrape(character).Cast<LimitBreak>().First();

            Assert.That(limitBreakResultData.Name, Is.EqualTo("Limit Break"));
            Assert.That(limitBreakResultData.FramesToCharge, Is.EqualTo("400"));
            Assert.That(limitBreakResultData.PercentDealtToCharge, Is.EqualTo("250%"));
            Assert.That(limitBreakResultData.PercentTakenToCharge, Is.EqualTo("100%"));
            Assert.That(limitBreakResultData.RunSpeed, Is.EqualTo("2.167"));
            Assert.That(limitBreakResultData.SHAirTime, Is.EqualTo("33 frames"));
            Assert.That(limitBreakResultData.WalkSpeed, Is.EqualTo("1.265"));
            Assert.That(limitBreakResultData.AirAcceleration, Is.EqualTo("0.072"));
            Assert.That(limitBreakResultData.AirSpeed, Is.EqualTo("1.32"));
            Assert.That(limitBreakResultData.FallSpeed, Is.EqualTo("1.848"));
            Assert.That(limitBreakResultData.FHAirTime, Is.EqualTo("47 frames"));
            Assert.That(limitBreakResultData.GainedPerFrame, Is.EqualTo("0.25"));
            Assert.That(limitBreakResultData.Gravity, Is.EqualTo("0.1078"));
        }

        [Test]
        public void Shulk_Monado_Data_Returns_Monado_Arts_Ultimate_Table_Data()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);
            var uniqueDataProvider = new DefaultUniqueDataProvider(instanceIdGenerator);
            var scrapingServices = new DefaultUniqueDataScrapingServices(uniqueDataProvider, webServices);

            var monadoArtsScraper = new MonadoArtsScraper(scrapingServices);

            var character = Characters.Shulk;
            character.Game = Games.Ultimate;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Ultimate/{character.EscapedCharacterName}";

            var monadoArtsResultData = monadoArtsScraper.Scrape(character).Cast<MonadoArt>();

            foreach (var monadoArt in monadoArtsResultData)
            {
                Assert.That(monadoArt.Name, Is.Not.Null);
                Assert.That(monadoArt.Active, Is.Not.Null);
                Assert.That(monadoArt.AirSlashHeight, Is.Not.Null);
                Assert.That(monadoArt.AirSpeed, Is.Not.Null);
                Assert.That(monadoArt.Cooldown, Is.Not.Null);
                Assert.That(monadoArt.DamageDealt, Is.Not.Null);
                Assert.That(monadoArt.DamageTaken, Is.Not.Null);
                Assert.That(monadoArt.FallSpeed, Is.Not.Null);
                Assert.That(monadoArt.Gravity, Is.Not.Null);
                Assert.That(monadoArt.InitialDashSpeed, Is.Not.Null);
                Assert.That(monadoArt.InstanceId, Is.Not.Null);
                Assert.That(monadoArt.JumpHeight, Is.Not.Null);
                Assert.That(monadoArt.KnockbackDealt, Is.Not.Null);
                Assert.That(monadoArt.KnockbackTaken, Is.Not.Null);
                Assert.That(monadoArt.LedgeJumpHeight, Is.Not.Null);
                Assert.That(monadoArt.Owner, Is.Not.Null);
                Assert.That(monadoArt.OwnerId, Is.Not.Null);
                Assert.That(monadoArt.RunSpeed, Is.Not.Null);
                Assert.That(monadoArt.ShieldHealth, Is.Not.Null);
                Assert.That(monadoArt.ShieldRegen, Is.Not.Null);
                Assert.That(monadoArt.WalkSpeed, Is.Not.Null);
            }
        }

        [Test]
        public void Shulk_Monado_Data_Returns_Monado_Arts_Smash4_Table_Data()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);
            var uniqueDataProvider = new DefaultUniqueDataProvider(instanceIdGenerator);
            var scrapingServices = new DefaultUniqueDataScrapingServices(uniqueDataProvider, webServices);

            var monadoArtsScraper = new MonadoArtsScraper(scrapingServices);

            var character = Characters.Shulk;
            character.Game = Games.Smash4;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Smash4/{character.EscapedCharacterName}";

            var monadoArtsResultData = monadoArtsScraper.Scrape(character).Cast<MonadoArt>();

            foreach (var monadoArt in monadoArtsResultData)
            {
                Assert.That(monadoArt.Name, Is.Not.Null);
                Assert.That(monadoArt.AirSpeed, Is.Not.Null);
                Assert.That(monadoArt.DamageDealt, Is.Not.Null);
                Assert.That(monadoArt.DamageTaken, Is.Not.Null);
                Assert.That(monadoArt.FallSpeed, Is.Not.Null);
                Assert.That(monadoArt.InstanceId, Is.Not.Null);
                Assert.That(monadoArt.JumpHeight, Is.Not.Null);
                Assert.That(monadoArt.KnockbackDealt, Is.Not.Null);
                Assert.That(monadoArt.KnockbackTaken, Is.Not.Null);
                Assert.That(monadoArt.Owner, Is.Not.Null);
                Assert.That(monadoArt.OwnerId, Is.Not.Null);
                Assert.That(monadoArt.ShieldHealth, Is.Not.Null);
                Assert.That(monadoArt.WalkSpeed, Is.Not.Null);
            }
        }

        //private static void AssertThatUniqueDataDictionaryHasKeyWithNonEmptyValue(IEnumerable<IUniqueData> uniqueDataList, string keyUnderTest)
        //{
        //    var uniqueData = uniqueDataList.FirstOrDefault(u => u.Name.Equals(keyUnderTest, StringComparison.OrdinalIgnoreCase));

        //    Assert.That(uniqueData, Is.Not.Null, $"does not contain item with {nameof(IUniqueData.Name)} '{keyUnderTest}'");

        //    // ReSharper disable once PossibleNullReferenceException
        //    //Assert.That(uniqueData.Value, Is.Not.Empty);
        //}
    }
}
