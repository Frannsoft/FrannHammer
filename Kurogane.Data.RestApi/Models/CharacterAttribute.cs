using System;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Models
{
    public class CharacterAttribute
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public CharacterAttributes AttributeType { get; set; }

        public CharacterAttribute(string rank, string characterName,
            string name, string value, CharacterAttributes attributeType)
        {
            Rank = rank;
            OwnerId = GetOwnerIdFromCharacterName(characterName);
            Name = name;
            Value = value;
            AttributeType = attributeType;
        }

        public CharacterAttribute()
        { }

        private int GetOwnerIdFromCharacterName(string name)
        {
            var characterId = (Characters)Enum.Parse(typeof(Characters), name, true);
            return (int)characterId;
        }
    }
}