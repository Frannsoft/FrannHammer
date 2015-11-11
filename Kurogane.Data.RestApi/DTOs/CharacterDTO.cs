using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
    }
}
