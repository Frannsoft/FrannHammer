using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public class WebCharacter : ICharacter
    {
        protected const string SourceUrlBase = "http://kuroganehammer.com/Smash4/";

        public string SourceUrl { get; private set; }
        public int Id { get; set; }
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

        public WebCharacter(string name, string escapedCharacterName = "")
        {
            Name = name;

            if(string.IsNullOrEmpty(escapedCharacterName))
            { escapedCharacterName = name; }

            SourceUrl = SourceUrlBase + escapedCharacterName;
        }

        public virtual void ScrapeMoves()
        {
            throw new NotImplementedException();
        }

        public virtual void ScrapeMovements()
        {
            throw new NotImplementedException();
        }

        public virtual void GetAttributes()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IUniqueData> GetUniqueData(IUniqueDataProvider uniqueDataProvider)
        {
            throw new NotImplementedException();
        }
    }
}
