using FrannHammer.Domain.Contracts;

namespace FrannHammer.NetCore.WebApi.Models
{
    public class CharacterResource : Resource, ICharacter
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string FullUrl { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ColorTheme { get; set; }
        public string DisplayName { get; set; }
        public Games Game { get; set; }
    }
}