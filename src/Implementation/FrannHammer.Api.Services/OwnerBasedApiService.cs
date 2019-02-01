using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System;
using System.Collections.Generic;

namespace FrannHammer.Api.Services
{
    public class OwnerBasedApiService<T> : BaseApiService<T>
        where T : IHaveAnOwner, IHaveAnOwnerId, IModel
    {
        public OwnerBasedApiService(IRepository<T> repository, IGameParameterParserService gameParameterParserService)
            : base(repository, gameParameterParserService)
        { }

        public virtual IEnumerable<T> GetAllWhereCharacterNameIs(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            return GetAllWhere(item => item.Owner.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public virtual IEnumerable<T> GetAllWhereCharacterOwnerIdIs(int id)
        {
            return GetAllWhere(item => item.OwnerId == id);
        }
    }
}