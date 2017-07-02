using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebApi.Models
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