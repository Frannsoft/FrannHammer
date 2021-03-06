﻿using FrannHammer.Domain.Contracts;

namespace FrannHammer.NetCore.WebApi.Models
{
    public class UniqueDataResource : Resource, IUniqueData
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public string Value { get; set; }
    }
}