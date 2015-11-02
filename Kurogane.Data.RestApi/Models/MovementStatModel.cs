using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane.Data.RestApi.Models
{
    [Table("MovementStats")]
    public class MovementStatModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string RawName { get; set; }
    }
}
