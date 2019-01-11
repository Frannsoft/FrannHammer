using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Contracts.Images;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Domain.Contracts;
using HtmlAgilityPack;
using System.Diagnostics;

namespace FrannHammer.WebScraping.Character
{
    public class DefaultCharacterDataScrapingServices : ICharacterDataScrapingServices
    {
        private readonly IImageScrapingService _imageScrapingService;
        private readonly IMovementScraper _movementScraper;
        private readonly IEnumerable<IAttributeScraper> _attributeScrapers;
        private readonly ICharacterMoveScraper _characterMoveScraper;
        private readonly IWebServices _webServices;
        private readonly IUniqueDataScrapingServices _uniqueDataScrapingService;

        public DefaultCharacterDataScrapingServices(IImageScrapingService imageScrapingService, IMovementScraper movementScraper,
            IEnumerable<IAttributeScraper> attributeScrapers,
            ICharacterMoveScraper characterMoveScraper,
            IUniqueDataScrapingServices uniqueDataScrapingServices,
            IWebServices webServices)
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
        }

        public void PopulateCharacter(WebCharacter character)
        {
            const string srcAttributeKey = "src";
            const string characterNameKey = "[charactername]";
            var htmlParser = _webServices.CreateParserFromSourceUrl(character.SourceUrl);
            string displayNameHtml = htmlParser.GetSingle(ScrapingConstants.XPathFrameDataVersion);

            string displayName = GetCharacterDisplayName(displayNameHtml);

            var thumbnailHtmlParser = _webServices.CreateParserFromSourceUrl(WebCharacter.SourceUrlBase);
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
                        thumbnailUrl = GetThumbnailUrl(srcAttributeKey, thumbnailHtml, WebCharacter.SourceUrlBase);
                        break;
                    }
                }
            }
            else
            {
                thumbnailUrl = GetThumbnailUrl(srcAttributeKey, thumbnailHtml, WebCharacter.SourceUrlBase);
            }

            //character details
            string mainImageUrl = htmlParser.GetAttributeFromSingleNavigable(srcAttributeKey, ScrapingConstants.XPathImageUrl);
            Uri mainImageUri;
            if (!Uri.TryCreate(mainImageUrl, UriKind.Absolute, out mainImageUri)) //add base address if it doesn't exist
            {
                mainImageUrl = WebCharacter.SourceUrlBase + mainImageUrl;
            }

            //color hex
            string colorTheme = _imageScrapingService.GetColorHexValue(mainImageUrl).Result;

            //movements
            var movements = _movementScraper.GetMovementsForCharacter(character);

            //moves
            var moves = _characterMoveScraper.ScrapeMoves(character);

            //attributes
            var attributeRows = new List<ICharacterAttributeRow>();
            foreach (var attributeScraper in _attributeScrapers)
            {
                attributeRows.AddRange(attributeScraper.Scrape(character));
            }

            //unique data
            var uniqueData = new List<IUniqueData>();
            var uniqueDataScrapersForThisCharacter = GetUniqueDataScrapersForCharacter(character);
            foreach (var uniqueDataScraper in uniqueDataScrapersForThisCharacter)
            {
                uniqueData.AddRange(uniqueDataScraper.Scrape(character));
            }

            character.FullUrl = character.SourceUrl;
            character.DisplayName = displayName;
            character.ThumbnailUrl = thumbnailUrl;
            character.MainImageUrl = mainImageUrl;
            character.ColorTheme = colorTheme;
            character.Movements = movements;
            character.Moves = moves;
            character.AttributeRows = attributeRows;
            character.UniqueProperties = uniqueData;
        }

        private IEnumerable<IUniqueDataScraper> GetUniqueDataScrapersForCharacter(WebCharacter character)
        {
            var scraperTypes = character.UniqueScraperTypes;

            return scraperTypes.Select(sc => (IUniqueDataScraper) Activator.CreateInstance(sc, _uniqueDataScrapingService));
        }

        private static string GetThumbnailUrl(string attributeKey, string thumbnailHtml, string urlRoot)
        {
            string thumbnailUriFromSource = HtmlNode.CreateNode(thumbnailHtml).GetAttributeValue(attributeKey, string.Empty);

            //dump the '/smash4' at the start of these if it exists.  This helps ensure scraped urls are sanitized and valid.
            if (thumbnailUriFromSource.StartsWith("/Smash4/"))
            {
                thumbnailUriFromSource = thumbnailUriFromSource.Remove(0, 8);
            }
            return urlRoot + thumbnailUriFromSource;
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
