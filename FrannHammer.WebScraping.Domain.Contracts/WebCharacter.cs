using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain.Contracts
{
    public class WebCharacter
    {
        protected const string SourceUrlBase = "http://kuroganehammer.com/Smash4/";

        public string SourceUrl { get; private set; }

        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ColorHex { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<IMove> Moves { get; set; }
        public IEnumerable<IMovement> Movements { get; set; }
        public IEnumerable<IAttribute> Attributes { get; set; }

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
