using FrannHammer.Domain.Contracts;
using static FrannHammer.Domain.FriendlyNameCommonConstants;

namespace FrannHammer.Domain
{
    public class Character : MongoModel, ICharacter
    {
        [FriendlyName("fullUrl")]
        public string FullUrl { get; set; }

        [FriendlyName("mainImageUrl")]
        public string MainImageUrl { get; set; }

        [FriendlyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [FriendlyName("colorTheme")]
        public string ColorTheme { get; set; }

        [FriendlyName("displayName")]
        public string DisplayName { get; set; }

        [FriendlyName(OwnerIdName)]
        public int OwnerId { get; set; }


        public Games Game { get; set; }
    }
}
