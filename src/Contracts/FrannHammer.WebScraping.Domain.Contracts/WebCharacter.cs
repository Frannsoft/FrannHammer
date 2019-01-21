using FrannHammer.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public class WebCharacter : ICharacter
    {
#if DEBUG
        //public const string SourceUrlBase = "http://localhost:81/khmock/kuroganehammer.com/Smash4/";
        public const string SourceSmash4UrlBase = "http://kuroganehammer.com/Smash4/";
        public const string SourceUltimateUrlBase = "http://kuroganehammer.com/Ultimate/";
#else
        public const string SourceSmash4UrlBase = "http://kuroganehammer.com/Smash4/";
        public const string SourceUltimateUrlBase = "http://kuroganehammer.com/Ultimate/";
#endif
        public string[] PotentialScrapingNames { get; }

        public string InstanceId { get; set; }
        public string SourceUrl { get; set; }
        public IReadOnlyCollection<string> SourceUrls { get; }
        public string FullUrl { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public Games Game { get; set; }
        public string EscapedCharacterName { get; set; }

        private string _displayName;
        public string DisplayName
        {
            //appveyor doesn't support c# 7 yet?
            // ReSharper disable once ArrangeAccessorOwnerBody
            get { return string.IsNullOrEmpty(_displayName) ? Name : _displayName; }
            // ReSharper disable once ArrangeAccessorOwnerBody
            set { _displayName = value; }
        }

        public IEnumerable<IMove> Moves { get; set; }
        public IEnumerable<IMovement> Movements { get; set; }
        public IEnumerable<IAttribute> Attributes { get; set; }
        public IEnumerable<ICharacterAttributeRow> AttributeRows { get; set; }
        public IEnumerable<IUniqueData> UniqueProperties { get; set; }

        /// <summary>
        /// The non-incrementing Id used to identity this character resource from an api consumer perspective.
        /// </summary>
        public int OwnerId { get; set; }

        public IEnumerable<Type> UniqueScraperTypes { get; }

        public WebCharacter(string name, string escapedCharacterName = "",
            IEnumerable<Type> uniqueScraperTypes = null, params string[] potentialScrapingNames)
        {
            Name = name;


            if (string.IsNullOrEmpty(escapedCharacterName))
            { escapedCharacterName = name; }

            EscapedCharacterName = escapedCharacterName;
            SourceUrls = new List<string> { SourceSmash4UrlBase, SourceUltimateUrlBase }.Select(url => url + escapedCharacterName).ToList();
            //SourceUrl = sourceBaseUrl + escapedCharacterName;//SourceSmash4UrlBase + escapedCharacterName;
            PotentialScrapingNames = potentialScrapingNames;
            UniqueScraperTypes = uniqueScraperTypes ?? new List<Type>();

            OwnerId = CharacterIds.FindByName(name);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}
