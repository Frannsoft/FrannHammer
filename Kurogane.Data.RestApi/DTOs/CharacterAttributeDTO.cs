using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterAttributeRowDTO
    {
        public string Rank { get; set; }
        public int SmashAttributeType { get; set; }
        public int OwnerId { get; set; }
        public string CharacterName { get; set; }
        public string ThumbnailUrl { get; set; }
        public IEnumerable<string> RawHeaders { get; set; }
        public IEnumerable<CharacterAttribute> RawValues { get; set; }
        public Dictionary<string, CharacterAttribute> ParsedValues { get; set; }

        public CharacterAttributeRowDTO(string rank, int attributeType, int ownerId, Dictionary<string, CharacterAttribute> values,
            ICharacterStatService characterStatService)
        {
            Rank = rank;
            SmashAttributeType = attributeType;
            OwnerId = ownerId;
            ParsedValues = values;
            RawHeaders = values.Keys;
            RawValues = values.Values;

            var character = characterStatService.GetCharacter(ownerId);
            CharacterName = character.Name;
            ThumbnailUrl = character.ThumbnailUrl;
        }
    }

    public class CharacterAttributeDTO : BaseDTO
    {
        public string Rank { get; set; }
        public string Value { get; set; }
        public string AttributeType { get; set; }

        public CharacterAttributeDTO(CharacterAttribute attribute, ICharacterStatService characterStatService)
            : base(attribute, characterStatService)
        {
            Rank = attribute.Rank;
            Value = attribute.Value;
            AttributeType = attribute.SmashAttributeTypeId.ToString();
        }

        public CharacterAttributeDTO()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;
            var compAttribute = obj as CharacterAttributeDTO;

            if (compAttribute == null) return retVal;
            if (Name.Equals(compAttribute.Name) &&
                Rank.Equals(compAttribute.Rank) &&
                Value.Equals(compAttribute.Value) &&
                AttributeType.Equals(compAttribute.AttributeType))
            {
                retVal = true;
            }

            return retVal;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return new
            {
                Id,
                Name,
                Value,
                Rank,
                AttributeType
            }.GetHashCode() + base.GetHashCode();
        }
    }
}