using System;

namespace FrannHammer.Models
{
    public class ThrowType : ThrowTypeBaseModel, IEntity
    {
        public DateTime LastModified { get; set; }
    }

}
