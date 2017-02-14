using System;

namespace FrannHammer.Models
{
    public class Movement : BaseMovementModel, IEntity
    {
        public DateTime LastModified { get; set; }
    }
}