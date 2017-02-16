using System;

namespace FrannHammer.Models
{
    public class CharacterAttribute : BaseCharacterAttributeModel, IMoveEntity
    {
        public DateTime LastModified { get; set; }
        public SmashAttributeType SmashAttributeType { get; set; }
        public CharacterAttributeType CharacterAttributeType { get; set; }
    }
}