using System.Collections.Generic;

namespace FrannHammer.Domain.Contracts
{
    public interface ICharacterAttributeRow : IModel, IHaveAnOwner
    {
        IEnumerable<IAttribute> Values { get; set; }
    }
}
