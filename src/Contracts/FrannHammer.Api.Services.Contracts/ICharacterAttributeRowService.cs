using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeRowService : ICrudService<ICharacterAttributeRow>
    {
        IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterNameIs(string name);
        IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterOwnerIdIs(int id);
        IEnumerable<ICharacterAttributeName> GetAllTypes();
        ICharacterAttributeRow GetSingleWithNameAndMatchingCharacterOwnerId(string name, int id);
        ICharacterAttributeRow GetSingleWithNameAndMatchingCharacterOwner(string attributeName, string name);
    }
}
