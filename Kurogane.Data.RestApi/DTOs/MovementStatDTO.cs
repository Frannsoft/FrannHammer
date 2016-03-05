
using System.Diagnostics.CodeAnalysis;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;

namespace Kurogane.Data.RestApi.DTOs
{
    public class MovementStatDto : BaseDto
    {
        public double Value { get; set; }

        public MovementStatDto(MovementStat movement, ICharacterStatService characterStatService)
            : base(movement, characterStatService)
        {
            Value = movement.Value;
        }

        public MovementStatDto()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;
            var compMovement = obj as MovementStatDto;

            if(compMovement != null)
            {
                if(Value.Equals(compMovement.Value) &&
                    base.Equals(compMovement))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return new
            {
                Value
            }.GetHashCode() + base.GetHashCode();
        }
    }
}
