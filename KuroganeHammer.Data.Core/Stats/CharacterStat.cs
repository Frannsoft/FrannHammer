using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("Characters")]
    public class CharacterStat
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Key]
        public int Id { get; set; }

        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
    }
}
