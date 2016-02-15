
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Providers;

namespace Kurogane.Data.RestApi.DTOs
{
    public class MovementStatDTO : BaseDTO
    {
        public double Value { get; set; }

        public MovementStatDTO(MovementStat movement, ICharacterStatService characterStatService)
            : base(movement, characterStatService)
        {
            Value = movement.Value;
        }
    }
}
