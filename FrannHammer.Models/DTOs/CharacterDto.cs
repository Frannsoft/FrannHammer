using System;

namespace FrannHammer.Models.DTOs
{
    [Obsolete("No point in using this right now")]
    public class CharacterDto
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

        //public CharacterDto(int id, string style, string mainImageUrl, string thumbNailUrl, string description,
        //    string name, string colorTheme)
        //{
        //    Id = id;
        //    Style = style;
        //    MainImageUrl = mainImageUrl;
        //    ThumbnailUrl = thumbNailUrl;
        //    Description = description;
        //    ColorTheme = colorTheme;
        //    Name = name;
        //}
    }
}
