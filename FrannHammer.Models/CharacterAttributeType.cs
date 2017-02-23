using System;

namespace FrannHammer.Models
{
    public class CharacterAttributeType : BaseCharacterAttributeTypeModel, IEntity
    {
        public Notation Notation { get; set; }
        public DateTime LastModified { get; set; }
    }
}