using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullUrl { get; set; }
        public int OwnerId { get; set; }
        public string FrameDataVersion { get; set; }
    }
}
