using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public class WebCharacter : ICharacter
    {
#if DEBUG
        public const string SourceUrlBase = "http://localhost:81/khmock/kuroganehammer.com/Smash4/";
#else
        public const string SourceUrlBase = "http://kuroganehammer.com/Smash4/";
#endif
        public string[] PotentialScrapingNames { get; }

        public string InstanceId { get; set; }
        public string SourceUrl { get; private set; }
        public string FullUrl { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DisplayName { get; set; }
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

            SourceUrl = SourceUrlBase + escapedCharacterName;
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
