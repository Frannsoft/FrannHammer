using System;

namespace FrannHammer.Models
{
    public class Move : BaseMoveModel, IMoveEntity
    {
        public DateTime LastModified { get; set; }
    }
}