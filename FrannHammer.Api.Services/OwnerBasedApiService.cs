using System.Collections.Generic;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class OwnerBasedApiService<T> : BaseApiService<T>
        where T : IHaveAnOwner, IHaveAnOwnerId, IModel
    {
        public OwnerBasedApiService(IRepository<T> repository) 
            : base(repository)
        { }

        public virtual IEnumerable<T> GetAllWhereCharacterNameIs(string name, string fields = "")
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            return GetAllWhere(item => EqualityComparer<string>.Default.Equals(item.Owner, name));
        }

        public virtual IEnumerable<T> GetAllWhereCharacterOwnerIdIs(int id, string fields = "")
        {
            return GetAllWhere(item => item.OwnerId == id);
        }
    }
}