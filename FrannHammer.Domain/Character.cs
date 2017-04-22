using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class Character : ICharacter
    {
        [FriendlyName("fullUrl")]
        public string FullUrl { get; set; }

        [FriendlyName("style")]
        public string Style { get; set; }

        [FriendlyName("mainImageUrl")]
        public string MainImageUrl { get; set; }

        [FriendlyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [FriendlyName("description")]
        public string Description { get; set; }

        [FriendlyName("colorTheme")]
        public string ColorTheme { get; set; }

        [FriendlyName("displayName")]
        public string DisplayName { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Id)]
        public int Id { get; set; }

        [FriendlyName(FriendlyNameCommonConstants.Name)]
        public string Name { get; set; }
    }
}
