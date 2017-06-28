using System;
using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : OwnerBasedApiService<ICharacterAttributeRow>, ICharacterAttributeRowService
    {
        public DefaultCharacterAttributeService(IRepository<ICharacterAttributeRow> characterAttributeRowRepository)
            : base(characterAttributeRowRepository)
        { }

        public override IEnumerable<ICharacterAttributeRow> GetAllWhereCharacterNameIs(string name, string fields = "")
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            return GetAllWhere(item => item.Owner.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
