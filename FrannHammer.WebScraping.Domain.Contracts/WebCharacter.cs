using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public class WebCharacter : ICharacter
    {
#if DEBUG
        public const string SourceUrlBase = "http://localhost:81/kuroganehammer.com/Smash4/";
#else
        public const string SourceUrlBase = "http://kuroganehammer.com/Smash4/";
#endif

        public string[] PotentialScrapingNames { get; }

        public string Id { get; set; }
        public string SourceUrl { get; private set; }
        public string FullUrl { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<IMove> Moves { get; set; }
        public IEnumerable<IMovement> Movements { get; set; }
        public IEnumerable<IAttribute> Attributes { get; set; }
        public IEnumerable<ICharacterAttributeRow> AttributeRows { get; set; }

        public WebCharacter(string name, string escapedCharacterName = "", params string[] potentialScrapingNames)
        {
            Name = name;

            if (string.IsNullOrEmpty(escapedCharacterName))
            { escapedCharacterName = name; }

            SourceUrl = SourceUrlBase + escapedCharacterName;
            PotentialScrapingNames = potentialScrapingNames;
        }

        public virtual IEnumerable<IUniqueData> GetUniqueData(IUniqueDataProvider uniqueDataProvider)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}
