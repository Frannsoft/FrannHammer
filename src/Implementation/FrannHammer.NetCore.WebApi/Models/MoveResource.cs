using FrannHammer.Domain.Contracts;
using Newtonsoft.Json;

namespace FrannHammer.NetCore.WebApi.Models
{
    public interface IMoveResource : IResource, IMove
    { }

    public class MoveResource : Resource, IMoveResource
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public string HitboxActive { get; set; }
        public string FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public string MoveType { get; set; }
        public bool IsWeightDependent { get; set; }
    }

    public class ExpandedMoveResource : MoveResource
    {
        public new ExpandedHitboxResource HitboxActive { get; set; }
        public new ExpandedBaseDamageResource BaseDamage { get; set; }
    }

    public class ExpandedBaseDamageResource
    {
        public string Normal { get; set; }

        [JsonProperty("OneVOne")]
        public string Vs1 { get; set; }

    }

    public class ExpandedHitboxResource
    {
        public string Frames { get; set; }
        public string Adv { get; set; } = string.Empty;
        public string SD { get; set; } = string.Empty;
        public string ShieldstunMultiplier { get; set; } = string.Empty;
        public string RehitRate { get; set; } = string.Empty;
        public string FacingRestrict { get; set; } = string.Empty;
        public string SuperArmor { get; set; } = string.Empty;
        public string HeadMultiplier { get; set; } = string.Empty;
        public string Intangible { get; set; } = string.Empty;
        public bool SetWeight { get; set; } = false;
        public bool GroundOnly { get; set; } = false;

    }
}