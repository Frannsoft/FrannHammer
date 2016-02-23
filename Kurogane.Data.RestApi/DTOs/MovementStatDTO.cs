
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

        public MovementStatDTO()
        { }

        public override bool Equals(object obj)
        {
            bool retVal = false;
            MovementStatDTO compMovement = obj as MovementStatDTO;

            if(compMovement != null)
            {
                if(this.Value.Equals(compMovement.Value) &&
                    base.Equals(compMovement))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public override int GetHashCode()
        {
            return new
            {
                Value
            }.GetHashCode() + base.GetHashCode();
        }
    }
}
