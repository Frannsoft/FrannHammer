using System;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Models
{
    public class CharacterAttribute : Stat
    {
        public string Rank { get; set; }
        public string Value { get; set; }
        public int SmashAttributeTypeId { get; set; }

        public CharacterAttribute(string rank, string characterName,
            string name, string value, SmashAttributeType smashAttributeType)
        {
            Rank = rank;
            OwnerId = GetOwnerIdFromCharacterName(characterName);
            Name = name;
            Value = value;
            SmashAttributeTypeId = smashAttributeType.Id;
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