using System.Collections.Generic;

namespace FrannHammer.Domain.Contracts
{
    public interface ICharacterAttributeRow
    {
        string CharacterName { get; }
        IEnumerable<IAttribute> Values { get; }
    }
}
