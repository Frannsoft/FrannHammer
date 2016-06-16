using System;

namespace KuroganeHammer.Data.Core.Models
{
    public class Movement
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string Value { get; set; }
    }
}