using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.NetCore.WebApi.Models
{
    public class CharacterAttributeRowResource : Resource, ICharacterAttributeRow
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public IEnumerable<IAttribute> Values { get; set; }
    }
}