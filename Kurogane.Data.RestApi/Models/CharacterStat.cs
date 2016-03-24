namespace Kurogane.Data.RestApi.Models
{
    public class CharacterStat : Stat
    {
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }

        public CharacterStat(string name, int ownerId, string style, string mainImageUrl,
            string thumbnailUrl, string colorTheme, string description)
            : base(name, ownerId)
        {
            Style = style;
            MainImageUrl = mainImageUrl;
            ThumbnailUrl = thumbnailUrl;
            Description = description;
            ColorTheme = colorTheme;
        }

        public CharacterStat()
        { }

    }
}
