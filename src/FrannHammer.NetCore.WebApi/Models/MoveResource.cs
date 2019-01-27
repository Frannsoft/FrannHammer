﻿using FrannHammer.Domain.Contracts;
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
        public string Game { get; set; }
    }

    public class UltimateMoveResource : MoveResource
    {
        public new HitboxResource HitboxActive { get; set; }
        public new BaseDamageResource BaseDamage { get; set; }
    }

    public class BaseDamageResource
    {
        public string Normal { get; set; }

        [JsonProperty("OneVOne")]
        public string Vs1 { get; set; }
    }

    public class HitboxResource
    {
        public string Frames { get; set; }

        [JsonProperty("Adv")]
        public string Adv { get; set; }
    }
}