using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterAttributeRowService : ICrudService<ICharacterAttributeRow>
    {
        IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterNameIs(string name, string fields = "");
    }
}
