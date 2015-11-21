using KuroganeHammer.Model;
using KuroganeHammer.Service;

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
