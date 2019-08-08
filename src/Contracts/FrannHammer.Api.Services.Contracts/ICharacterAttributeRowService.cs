using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeRowService : ICrudService<ICharacterAttributeRow>
    {
        IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterNameIs(string name);
        IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterOwnerIdIs(int id);
        IEnumerable<ICharacterAttributeName> GetAllTypes();
        IEnumerable<ICharacterAttributeRow> GetSingleWithNameAndMatchingCharacterOwnerId(string name, int id);
        IEnumerable<ICharacterAttributeRow> GetAllWithNameAndMatchingCharacterOwner(string attributeName, string name);
    }
}
