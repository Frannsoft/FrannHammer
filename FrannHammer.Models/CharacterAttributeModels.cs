using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrannHammer.Models
{
    public abstract class BaseCharacterAttribute : BaseModel
    {
        public Character Character { get; set; }
        public int CharacterId { get; set; }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            throw new NotImplementedException("No support for updating these individual attributes yet.  " +
                                              "To be clear, the controllers and API exist, just not the helper/wrapper methods");
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            throw new NotImplementedException("No support for deleting these individual attributes yet.  " +
                                              "To be clear, the controllers and API exist, just not the helper/wrapper methods");
        }

        public override async Task<HttpResponseMessage> Create(HttpClient client)
        {
            return await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/characterattributes", this);
        }
    }

    public class AirAcceleration : BaseCharacterAttribute
    {
        public double Value { get; set; }
    }

    public class AirDeceleration : BaseCharacterAttribute
    {
        public double Value { get; set; }
    }

    public class AirDodge : BaseCharacterAttribute
    {
        public string Intangible { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class AirFriction : BaseCharacterAttribute
    {
        public double Value { get; set; }
    }

    public class AirSpeed : BaseCharacterAttribute
    {
        public double MaxAirSpeedValue { get; set; }
    }

    public class Counter : BaseCharacterAttribute
    {
        public string Frames { get; set; }
        public string Intangible { get; set; }
        public int FirstActiveFrame { get; set; }
        public double DamageMultiplier { get; set; }
        public BaseKnockback BaseKnockback { get; set; }
        public SetKnockback SetKnockback { get; set; }
        public int BaseKnockbackSetKnockbackId { get; set; }
        public KnockbackGrowth KnockbackGrowth { get; set; }
        public int KnockbackGrowthId { get; set; }
    }

    public class DashLength : BaseCharacterAttribute
    {
        public int FramesLength { get; set; }
    }

    public class FallSpeed : BaseCharacterAttribute
    {
        public double MaxFallSpeed { get; set; }
        public double FastFallSpeed { get; set; }
        public double SpeedIncrease { get; set; }
    }

    public class Gravity : BaseCharacterAttribute
    {
        public double Value { get; set; }
    }

    public class Jumpsquat : BaseCharacterAttribute
    {
        public double Value { get; set; }
    }

    public class LedgeAttack : BaseCharacterAttribute
    {
        public Hitbox Hitbox { get; set; }
        public int HitboxId { get; set; }
        public int FirstActiveFrame { get; set; }
        public double Damage { get; set; }
    }

    public class LedgeGetup : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class LedgeJump : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class LedgeRoll : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class Reflector : BaseCharacterAttribute
    {
        public string ReflectsOnFrames { get; set; }
        public Hitbox Hitbox { get; set; }
        public int HitboxId { get; set; }
        public double DamageMultiplier { get; set; }
        public double SpeedMultiplier { get; set; }
        public double BreakThreshold { get; set; }
    }

    public class Roll : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class RunSpeed : BaseCharacterAttribute
    {
        public double MaxRunSpeedValue { get; set; }
    }

    public class Shield : BaseCharacterAttribute
    {
        public double ShieldHp { get; set; }
        public double ResetHpWhenBroken { get; set; }
        public double DamageMultiplier { get; set; }
        public double HpRegeneratePerFrame { get; set; }
        public double HpDegeneratePerFrame { get; set; }
        public string PowerShieldFrames { get; set; }
    }

    public class Spotdodge : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class TechInPlace : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class TechRollForward : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class TechRollBackward : BaseCharacterAttribute
    {
        public string Intangibility { get; set; }
        public int FirstActiveFrame { get; set; }
    }

    public class Traction : BaseCharacterAttribute
    {
        public double Value { get; set; }
    }

    public class WalkSpeed : BaseCharacterAttribute
    {
        public double MaxWalkSpeedValue { get; set; }
    }

    public class Weight : BaseCharacterAttribute
    {
        public double WeightValue { get; set; }
    }

    public class BaseCharacterAttributeModel
    {
        protected bool Equals(BaseCharacterAttributeModel other)
        {
            return OwnerId == other.OwnerId && string.Equals(Rank, other.Rank)
                && string.Equals(Value, other.Value) && string.Equals(Name, other.Name)
                && Id == other.Id && SmashAttributeTypeId == other.SmashAttributeTypeId
                && CharacterAttributeTypeId == other.CharacterAttributeTypeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseCharacterAttributeModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = OwnerId;
                hashCode = (hashCode * 397) ^ (Rank != null ? Rank.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Id;
                hashCode = (hashCode * 397) ^ SmashAttributeTypeId;
                hashCode = (hashCode * 397) ^ CharacterAttributeTypeId.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(BaseCharacterAttributeModel left, BaseCharacterAttributeModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseCharacterAttributeModel left, BaseCharacterAttributeModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int SmashAttributeTypeId { get; set; }
        public int? CharacterAttributeTypeId { get; set; }
    }

    public class CharacterAttributeDto : BaseCharacterAttributeModel
    { }

    public class CharacterAttribute : BaseCharacterAttributeModel, IEntity
    {
        public DateTime LastModified { get; set; }
        public SmashAttributeType SmashAttributeType { get; set; }
        public CharacterAttributeType CharacterAttributeType { get; set; }
    }
}