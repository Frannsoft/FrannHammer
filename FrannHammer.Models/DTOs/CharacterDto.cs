namespace FrannHammer.Models.DTOs
{
    public class CharacterDto
    {
        //public string Style { get; }
        //public string MainImageUrl { get; }
        //public string ThumbnailUrl { get; }
        //public string Description { get; }
        //public string ColorTheme { get; }
        public string Name { get; }
        //public string DisplayName { get; }
        //public int Id { get; }

        //public CharacterDto(int id, string style, string mainImageUrl, string thumbNailUrl, string description,
        //    string colorTheme, string name)
        public CharacterDto(string name)
        {
            //Id = id;
            //Style = style;
            //MainImageUrl = mainImageUrl;
            //ThumbnailUrl = thumbNailUrl;
            //Description = description;
            //ColorTheme = colorTheme;
            Name = name;
        }
    }

    public class A
    {
        public string Name { get; set; }
    }

    public class ADto
    {
        public string Name { get; }

        public ADto(string name)
        {
            Name = name;
        }
    }
}
