using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Contracts.Images;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.PageDownloading;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrannHammer.WebScraping.Character
{
    public class DefaultCharacterDataScrapingServices : ICharacterDataScrapingServices
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;
        private readonly IColorScrapingService _imageScrapingService;
        private readonly IMovementScraper _movementScraper;
        private readonly IEnumerable<IAttributeScraper> _attributeScrapers;
        private readonly ICharacterMoveScraper _characterMoveScraper;
        private readonly IWebServices _webServices;
        private readonly IUniqueDataScrapingServices _uniqueDataScrapingService;

        public DefaultCharacterDataScrapingServices(IColorScrapingService imageScrapingService, IMovementScraper movementScraper,
            IEnumerable<IAttributeScraper> attributeScrapers,
            ICharacterMoveScraper characterMoveScraper,
            IUniqueDataScrapingServices uniqueDataScrapingServices,
            IWebServices webServices,
            IInstanceIdGenerator instanceIdGenerator)
        {
            Guard.VerifyObjectNotNull(imageScrapingService, nameof(imageScrapingService));
            Guard.VerifyObjectNotNull(movementScraper, nameof(movementScraper));
            Guard.VerifyObjectNotNull(attributeScrapers, nameof(attributeScrapers));
            Guard.VerifyObjectNotNull(characterMoveScraper, nameof(characterMoveScraper));
            Guard.VerifyObjectNotNull(uniqueDataScrapingServices, nameof(uniqueDataScrapingServices));
            Guard.VerifyObjectNotNull(webServices, nameof(webServices));

            _imageScrapingService = imageScrapingService;
            _movementScraper = movementScraper;
            _attributeScrapers = attributeScrapers;
            _characterMoveScraper = characterMoveScraper;
            _uniqueDataScrapingService = uniqueDataScrapingServices;
            _webServices = webServices;
            _instanceIdGenerator = instanceIdGenerator;
        }

        public WebCharacter PopulateCharacter(WebCharacter character, string sourceBaseUrl)
        {
            var populatedCharacter = new WebCharacter(character.Name, character.EscapedCharacterName, character.UniqueScraperTypes, character.PotentialScrapingNames);

            if (populatedCharacter.OwnerId != CharacterIds.RosalinaLuma || (populatedCharacter.OwnerId == CharacterIds.RosalinaLuma && sourceBaseUrl.Contains("Smash4")))
            {
                populatedCharacter.SourceUrl = sourceBaseUrl + populatedCharacter.EscapedCharacterName;
            }
            else if (populatedCharacter.OwnerId == CharacterIds.RosalinaLuma && sourceBaseUrl.Contains("Ultimate"))
            {
                populatedCharacter.SourceUrl = sourceBaseUrl + "Rosalina"; //hack, but having different names for same chars across games? come on.
            }
            populatedCharacter.CssKey = character.CssKey;

            const string srcAttributeKey = "src";
            const string characterNameKey = "[charactername]";
            var htmlParser = _webServices.CreateParserFromSourceUrl(populatedCharacter.SourceUrl);
            string displayNameHtml = htmlParser.GetSingle(ScrapingConstants.XPathFrameDataVersion);

            string displayName = GetCharacterDisplayName(displayNameHtml);

            if (displayName == "Rosalina")
            {
                displayName = "Rosalina & Luma"; //see previously mentioned hack comment
            }

            string thumbnailPreparedUrl = Regex.Replace(populatedCharacter.SourceUrl, "([^/]*)$", string.Empty);
            var thumbnailHtmlParser = _webServices.CreateParserFromSourceUrl(thumbnailPreparedUrl);
            string thumbnailHtml = thumbnailHtmlParser.GetSingle(ScrapingConstants.XPathThumbnailUrl.Replace(characterNameKey, character.DisplayName));

            string thumbnailUrl = string.Empty;

            if (string.IsNullOrEmpty(thumbnailHtml))
            {
                //try potential scraping names if there are any for the character
                foreach (string name in character.PotentialScrapingNames)
                {
                    thumbnailHtml = thumbnailHtmlParser.GetSingle(ScrapingConstants.XPathThumbnailUrl.Replace(characterNameKey, name));

                    if (!string.IsNullOrEmpty(thumbnailHtml))
                    {
                        thumbnailUrl = GetThumbnailUrl(srcAttributeKey, thumbnailHtml, populatedCharacter.SourceUrl);
                        break;
                    }
                }
            }
            else
            {
                thumbnailUrl = GetThumbnailUrl(srcAttributeKey, thumbnailHtml, populatedCharacter.SourceUrl);
            }

            //character details
            string mainImageUrl = htmlParser.GetAttributeFromSingleNavigable(srcAttributeKey, ScrapingConstants.XPathImageUrl);

            var sourceUri = new Uri(populatedCharacter.SourceUrl);
            string baseUri = sourceUri.GetLeftPart(UriPartial.Authority);

            if (!string.IsNullOrEmpty(mainImageUrl) && !mainImageUrl.StartsWith(baseUri))
            {
                mainImageUrl = baseUri + mainImageUrl;
            }

            //color hex
            string colorTheme = string.Empty;

            if (!string.IsNullOrEmpty(populatedCharacter.CssKey))
            {
                colorTheme = _imageScrapingService.GetColorHexValue(populatedCharacter.CssKey).Result;
            }

            //movements
            var movements = _movementScraper.GetMovementsForCharacter(populatedCharacter);

            //moves
            var moves = _characterMoveScraper.ScrapeMoves(populatedCharacter);

            //attributes
            var attributeRows = new List<ICharacterAttributeRow>();
            foreach (var attributeScraper in _attributeScrapers)
            {
                string attributeName = string.Empty;
                if (attributeScraper.AttributeName == "RunSpeed")
                {
                    attributeName = "DashSpeed"; //grrr..
                }
                else
                {
                    attributeName = attributeScraper.AttributeName;
                }

                attributeScraper.SourceUrl = $"{sourceBaseUrl}{attributeName}";

                try
                {
                    var newAttributes = attributeScraper.Scrape(populatedCharacter);
                    attributeRows.AddRange(newAttributes);
                }
                catch (PageNotFoundException ex)
                {
                    Console.WriteLine($"Exception while running scraper: {attributeScraper.AttributeName} for '{character.Name}'{Environment.NewLine}{ex.Message}");
                }
            }

            //unique data
            var uniqueData = new List<object>();
            var uniqueDataScrapersForThisCharacter = GetUniqueDataScrapersForCharacter(populatedCharacter);

            foreach (var uniqueDataScraper in uniqueDataScrapersForThisCharacter)
            {
                uniqueData.AddRange(uniqueDataScraper.Scrape(populatedCharacter));
            }

            populatedCharacter.InstanceId = _instanceIdGenerator.GenerateId();
            populatedCharacter.FullUrl = populatedCharacter.SourceUrl;
            populatedCharacter.DisplayName = displayName;
            populatedCharacter.ThumbnailUrl = thumbnailUrl;
            populatedCharacter.MainImageUrl = mainImageUrl;
            populatedCharacter.ColorTheme = colorTheme;
            populatedCharacter.Movements = movements;
            populatedCharacter.Moves = moves;
            populatedCharacter.AttributeRows = attributeRows;
            populatedCharacter.UniqueProperties = uniqueData;
            populatedCharacter.Game = populatedCharacter.SourceUrl.Contains("Ultimate") ? Games.Ultimate : Games.Smash4;
            return populatedCharacter;
        }

        private IEnumerable<IUniqueDataScraper> GetUniqueDataScrapersForCharacter(WebCharacter character)
        {
            var scraperTypes = character.UniqueScraperTypes;

            return scraperTypes.Select(sc => (IUniqueDataScraper)Activator.CreateInstance(sc, _uniqueDataScrapingService));
        }

        private static string GetThumbnailUrl(string attributeKey, string thumbnailHtml, string urlRoot)
        {
            string thumbnailUriFromSource = HtmlNode.CreateNode(thumbnailHtml).GetAttributeValue(attributeKey, string.Empty);

            thumbnailUriFromSource = "https://kuroganehammer.com" + thumbnailUriFromSource;
            return thumbnailUriFromSource;
        }

        private static string GetCharacterDisplayName(string rawDisplayNameHtml)
        {
            var node = HtmlNode.CreateNode(rawDisplayNameHtml);
            var characterFriendlyName = node.InnerText.Split(new[] { "'s F" }, StringSplitOptions.None)[0];

            //clean up and remove the "'s"
            string retVal = characterFriendlyName;
            if (characterFriendlyName.EndsWith("'s"))
            {
                retVal = characterFriendlyName.Remove(characterFriendlyName.Length - 2, 2);
            }
            return retVal;
        }
    }
}
