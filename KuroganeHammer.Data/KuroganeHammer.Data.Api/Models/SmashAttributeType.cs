using System;

namespace KuroganeHammer.Data.Api.Models
{
    public class SmashAttributeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public Notation Notation { get; set; }
        public int? NotationId { get; set; }
    }
}