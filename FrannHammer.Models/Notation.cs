using System;

namespace FrannHammer.Models
{
    public class Notation : BaseNotationModel, IEntity
    {
        public DateTime LastModified { get; set; }
    }
}