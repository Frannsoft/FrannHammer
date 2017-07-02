using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Domain
{
    public class CharacterDetailsDto : ICharacterDetailsDto
    {
        public ICharacter Metadata { get; set; }
        public IEnumerable<IMovement> Movements { get; set; }
        public IEnumerable<ICharacterAttributeRow> AttributeRows { get; set; }
    }
}
