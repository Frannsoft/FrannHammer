using System;

namespace FrannHammer.Models
{
    public abstract class BaseMeta : BaseModel
    {
        public int Id { get; set; }
        public Character Owner { get; set; }
        public int OwnerId { get; set; }
        public Move Move { get; set; }
        public int MoveId { get; set; }
        public string Notes { get; set; }
        public DateTime LastModified { get; set; }
        public string RawValue { get; set; }
    }
}