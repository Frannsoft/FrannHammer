using System;

namespace KuroganeHammer.Data.Api.Models
{
    public enum NotationTypes
    {
        Frames,
        Float,
        Boolean
    }

    public class Notation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NotationTypes NotationType { get; set; }
        public DateTime LastModified { get; set; }
    }
}