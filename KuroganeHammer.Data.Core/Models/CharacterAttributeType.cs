using System;

namespace KuroganeHammer.Data.Core.Models
{
    public class CharacterAttributeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Notation Notation { get; set; }
        public int? NotationId { get; set; }
        public DateTime LastModified { get; set; }
    }
}