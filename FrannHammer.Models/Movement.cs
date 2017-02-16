using System;

namespace FrannHammer.Models
{
    public class Movement : BaseMovementModel, IMoveEntity
    {
        public DateTime LastModified { get; set; }
    }
}