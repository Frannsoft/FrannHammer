using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurogane.Data.RestApi.Models
{
    [Table("Roster")]
    public class CharacterModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FullUrl { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string FrameDataVersion { get; set; }


    }
}
