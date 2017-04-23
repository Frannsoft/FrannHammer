using System.Collections.Generic;

namespace FrannHammer.Domain.Contracts
{
    public interface ICharacterAttributeRow : IModel
    {
        string CharacterName { get; }
        IEnumerable<IAttribute> Values { get; }
    }
}
