
using System.Diagnostics.CodeAnalysis;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;

namespace Kurogane.Data.RestApi.DTOs
{
    public class MovementStatDTO : BaseDTO
    {
        public string Value { get; set; }

        public MovementStatDTO(MovementStat movement, ICharacterStatService characterStatService)
            : base(movement, characterStatService)
        {
            Value = movement.Value;
        }

        public MovementStatDTO()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;
            var compMovement = obj as MovementStatDTO;

            if (compMovement == null) return retVal;
            if (Value.Equals(compMovement.Value) &&
                base.Equals(compMovement))
            {
                retVal = true;
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
