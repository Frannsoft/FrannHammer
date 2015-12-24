using KuroganeHammer.Model;
using KuroganeHammer.Service;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string Description { get; set; }

        public CharacterDTO(CharacterStat stat)
        {
            Style = stat.Style;
            Description = stat.Description;
            MainImageUrl = stat.MainImageUrl;
            Id = stat.Id;
            ThumbnailUrl = stat.ThumbnailUrl;
            OwnerId = stat.OwnerId;
            Name = stat.Name;
        }
    }
}
