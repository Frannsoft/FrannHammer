using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.Unique;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class UniqueDataScrapersTests
    {
        private readonly InstanceIdGenerator instanceIdGenerator;
        private readonly DefaultHtmlParserProvider htmlParserProvider;
        private readonly DefaultPageDownloader pageDownloader;
        private readonly DefaultWebClientProvider webClientProvider;
        private readonly DefaultWebServices webServices;
        private readonly DefaultUniqueDataProvider uniqueDataProvider;
        private readonly DefaultUniqueDataScrapingServices scrapingServices;

        public UniqueDataScrapersTests()
        {
            instanceIdGenerator = new InstanceIdGenerator();
            htmlParserProvider = new DefaultHtmlParserProvider();
            pageDownloader = new DefaultPageDownloader();
            webClientProvider = new DefaultWebClientProvider();
            webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);
            uniqueDataProvider = new DefaultUniqueDataProvider(instanceIdGenerator);
            scrapingServices = new DefaultUniqueDataScrapingServices(uniqueDataProvider, webServices);
        }

        [Test]
        [TestCase("Smash4")]
        public void ScrapeUniqueData_CloudLimitBreak_ReturnsLimitBreakTableData(string gameUrlModifier)
        {
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
        public void VegetableScraper_Returns_Daisy_Ultimate_Vegetable_Data()
        {
            var vegetableScraper = new VegetableScraper(scrapingServices);

            var character = Characters.Daisy;
            character.Game = Games.Ultimate;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Ultimate/{character.EscapedCharacterName}";

            var vegetableData = vegetableScraper.Scrape(character).Cast<Vegetable>().ToList();

            Assert.That(vegetableData.Count, Is.EqualTo(6));
            Assert.That(vegetableData[0].Name, Is.EqualTo("Vegetable - Normal"));
            Assert.That(vegetableData[1].Name, Is.EqualTo("Vegetable - Winking"));
            Assert.That(vegetableData[2].Name, Is.EqualTo("Vegetable - Dot Eyes"));
            Assert.That(vegetableData[3].Name, Is.EqualTo("Vegetable - Stitchface"));
            Assert.That(vegetableData[4].Name, Is.EqualTo("Vegetable - Bob-omb"));
            Assert.That(vegetableData[5].Name, Is.EqualTo("Vegetable - Mr. Saturn"));
            Assert.That(vegetableData[5].Chance, Is.EqualTo("1/166 (0.6%)"));
            Assert.That(vegetableData[5].DamageDealt, Is.EqualTo("6%"));
        }

        [Test]
        public void VegetableScraper_Returns_Peach_Smash4_Vegetable_Data()
        {
            var vegetableScraper = new VegetableScraper(scrapingServices);

            var character = Characters.Peach;
            character.Game = Games.Smash4;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Smash4/{character.EscapedCharacterName}";

            var vegetableData = vegetableScraper.Scrape(character).Cast<Vegetable>().ToList();

            Assert.That(vegetableData.Count, Is.EqualTo(6));
            Assert.That(vegetableData[0].Name, Is.EqualTo("Vegetable - Normal"));
            Assert.That(vegetableData[1].Name, Is.EqualTo("Vegetable - Winking"));
            Assert.That(vegetableData[2].Name, Is.EqualTo("Vegetable - Dot Eyes"));
            Assert.That(vegetableData[3].Name, Is.EqualTo("Vegetable - Stitchface"));
            Assert.That(vegetableData[4].Name, Is.EqualTo("Vegetable - Bob-omb"));
            Assert.That(vegetableData[5].Name, Is.EqualTo("Vegetable - Mr. Saturn"));
            Assert.That(vegetableData[5].Chance, Is.EqualTo("1/166"));
            Assert.That(vegetableData[5].DamageDealt, Is.EqualTo("6%"));
        }

        [Test]
        public void FloatScraper_Returns_Daisy_Ultimate_Float_Data()
        {
            var floatScraper = new FloatScraper(scrapingServices);

            var character = Characters.Daisy;
            character.Game = Games.Ultimate;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Ultimate/{character.EscapedCharacterName}";

            var floatResultData = floatScraper.Scrape(character).Cast<Float>().ToList();

            Assert.That(floatResultData.Count(), Is.EqualTo(1));

            Assert.That(floatResultData[0].Name, Is.EqualTo("Float"));
            Assert.That(floatResultData[0].Game, Is.EqualTo(Games.Ultimate));
            Assert.That(floatResultData[0].Owner, Is.EqualTo(character.Name));
            Assert.That(floatResultData[0].DurationInFrames, Is.EqualTo("150"));
            Assert.That(floatResultData[0].DurationInSeconds, Is.EqualTo("2.5"));
        }

        [Test]
        public void FloatScraper_Returns_Peach_Smash4_Float_Data()
        {
            var floatScraper = new FloatScraper(scrapingServices);

            var character = Characters.Peach;
            character.Game = Games.Ultimate;
            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Smash4/{character.EscapedCharacterName}";

            var floatResultData = floatScraper.Scrape(character).Cast<Float>().ToList();

            Assert.That(floatResultData.Count(), Is.EqualTo(1));

            Assert.That(floatResultData[0].Name, Is.EqualTo("Float"));
            Assert.That(floatResultData[0].Game, Is.EqualTo(Games.Smash4));
            Assert.That(floatResultData[0].Owner, Is.EqualTo(character.Name));
            Assert.That(floatResultData[0].DurationInFrames, Is.EqualTo("150"));
            Assert.That(floatResultData[0].DurationInSeconds, Is.EqualTo("2.5"));
        }

        [Test]
        public void Shulk_Monado_Data_Returns_Monado_Arts_Ultimate_Table_Data()
        {
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
    }
}
