using System.Collections.Generic;

namespace FrannHammer.Domain.Contracts
{
    public interface ICharacterDetailsDto
    {
        ICharacter Metadata { get; set; }
        IEnumerable<IMovement> Movements { get; set; }
        IEnumerable<ICharacterAttributeRow> AttributeRows { get; set; }
    }
}
