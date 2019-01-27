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

namespace FrannHammer.WebScraping.Character
{
    public class DefaultCharacterDataScrapingServices : ICharacterDataScrapingServices
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;
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
            populatedCharacter.SourceUrl = sourceBaseUrl + populatedCharacter.EscapedCharacterName;

            const string srcAttributeKey = "src";
            const string characterNameKey = "[charactername]";
            var htmlParser = _webServices.CreateParserFromSourceUrl(populatedCharacter.SourceUrl);//(character.SourceUrl);
            string displayNameHtml = htmlParser.GetSingle(ScrapingConstants.XPathFrameDataVersion);

            string displayName = GetCharacterDisplayName(displayNameHtml);

            var thumbnailHtmlParser = _webServices.CreateParserFromSourceUrl(populatedCharacter.SourceUrl.Replace($"/{populatedCharacter.Name}", string.Empty));//(WebCharacter.SourceSmash4UrlBase);
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
                        thumbnailUrl = GetThumbnailUrl(srcAttributeKey, thumbnailHtml, populatedCharacter.SourceUrl);//WebCharacter.SourceSmash4UrlBase);
                        break;
                    }
                }
            }
            else
            {
                thumbnailUrl = GetThumbnailUrl(srcAttributeKey, thumbnailHtml, populatedCharacter.SourceUrl);// WebCharacter.SourceSmash4UrlBase);
            }

            //character details
            string mainImageUrl = htmlParser.GetAttributeFromSingleNavigable(srcAttributeKey, ScrapingConstants.XPathImageUrl);
            Uri mainImageUri;
            if (!Uri.TryCreate(mainImageUrl, UriKind.Absolute, out mainImageUri)) //add base address if it doesn't exist
            {
                mainImageUrl = populatedCharacter.SourceUrl/*WebCharacter.SourceSmash4UrlBase*/ + mainImageUrl;
            }

            //color hex
            string colorTheme = _imageScrapingService.GetColorHexValue(mainImageUrl).Result;

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
            var uniqueData = new List<IUniqueData>();
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

            if (thumbnailUriFromSource.StartsWith("/Smash4/"))
            {
                thumbnailUriFromSource = "http://kuroganehammer.com" + thumbnailUriFromSource;
            }
            return thumbnailUriFromSource;//urlRoot + thumbnailUriFromSource;
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
