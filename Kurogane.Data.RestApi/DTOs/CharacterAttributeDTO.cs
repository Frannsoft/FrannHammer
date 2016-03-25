using System.Diagnostics.CodeAnalysis;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.DTOs
{
    public class CharacterAttributeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string AttributeType { get; set; }

        public CharacterAttributeDTO(CharacterAttribute attribute)
        {
            Id = attribute.Id;
            Name = attribute.Name;
            Value = attribute.Value;
            AttributeType = attribute.AttributeType.ToString();
        }

        public CharacterAttributeDTO()
        { }

        public override bool Equals(object obj)
        {
            var retVal = false;
            var compAttribute = obj as CharacterAttributeDTO;

            if (compAttribute == null) return retVal;
            if (Name.Equals(compAttribute.Name) &&
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
                AttributeType
            }.GetHashCode();
        }
    }
}