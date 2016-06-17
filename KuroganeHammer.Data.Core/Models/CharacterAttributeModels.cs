using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KuroganeHammer.Data.Core.Models
{
    public abstract class BaseCharacterAttribute : BaseModel
    {
        public int Id { get; set; }
        public Character Character { get; set; }
        public int CharacterId { get; set; }
        public DateTime LastModified { get; set; }

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


        public override Task<HttpResponseMessage> Create(HttpClient client)
        {
            throw new NotImplementedException();
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
        public BaseKnockbackSetKnockback BaseKnockbackSetKnockback { get; set; }
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

    public class CharacterAttribute
    {
        public int OwnerId { get; set; }
        public string Rank { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public SmashAttributeType SmashAttributeType { get; set; }
        public int SmashAttributeTypeId { get; set; }
        public CharacterAttributeType CharacterAttributeType { get; set; }
        public int? CharacterAttributeTypeId { get; set; }

        //public CharacterAttribute(string rank, string characterName,
        //    string name, string value, SmashAttributeType smashAttributeType)
        //{
        //    Rank = rank;
        //    OwnerId = GetOwnerIdFromCharacterName(characterName);
        //    Name = name;
        //    Value = value;
        //    SmashAttributeType = smashAttributeType;
        //}

        //public CharacterAttribute()
        //{ }

        //private int GetOwnerIdFromCharacterName(string name)
        //{
        //    var characterId = (Characters)Enum.Parse(typeof(Characters), name, true);
        //    return (int)characterId;
        //}
    }
}