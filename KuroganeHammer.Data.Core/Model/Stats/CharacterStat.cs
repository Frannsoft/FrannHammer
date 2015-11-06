using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuroganeHammer.Data.Core.Model.Stats
{
    [Table("Roster")]
    public class CharacterStat
    {
        public string FullUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Key]
        public int Id { get; set; }

        public string FrameDataVersion { get; set; }
    }
}
