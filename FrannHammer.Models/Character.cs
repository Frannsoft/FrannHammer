using System;

namespace FrannHammer.Models
{
    public class Character : BaseCharacterModel, IEntity
    {
        public DateTime LastModified { get; set; }
    }
}