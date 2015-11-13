using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurogane.Data.RestApi.DTOs
{
    public class MovementStatDTO : BaseDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string CharacterName { get; set; }
    }
}
