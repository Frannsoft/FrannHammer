using System.Collections.Generic;

namespace FrannHammer.Domain.Contracts
{
    public interface ICharacterAttributeRow : IModel
    {
        string CharacterName { get; set; }
        IEnumerable<IAttribute> Values { get; set; }
    }
}
