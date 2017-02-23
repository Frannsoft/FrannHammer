using System;

namespace FrannHammer.Models
{
    public class Throw : ThrowBaseModel, IMoveIdEntity
    {
        public Move Move { get; set; }
        public ThrowType ThrowType { get; set; }
        public DateTime LastModified { get; set; }
    }
}
