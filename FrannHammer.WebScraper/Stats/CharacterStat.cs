using System;

namespace FrannHammer.WebScraper.Stats
{
    public class CharacterStat
    {
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        public CharacterStat(int id, string name, string displayName, string style, string mainImageUrl,
            string thumbnailUrl, string colorTheme, string description, DateTime lastModified)
        {
            Name = name;
            DisplayName = displayName;
            Id = id;
            Style = style;
            MainImageUrl = mainImageUrl;
            ThumbnailUrl = thumbnailUrl;
            Description = description;
            ColorTheme = colorTheme;
            LastModified = lastModified;
        }

        public CharacterStat()
        { }

    }
}
