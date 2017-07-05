using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultUniqueDataService : OwnerBasedApiService<IUniqueData>, IUniqueDataService
    {
        private readonly IQueryMappingService _queryMappingService;

        public DefaultUniqueDataService(IRepository<IUniqueData> repository, IQueryMappingService queryMappingService)
            : base(repository)
        {
            Guard.VerifyObjectNotNull(queryMappingService, nameof(queryMappingService));
            _queryMappingService = queryMappingService;
        }

        public IEnumerable<IUniqueData> GetAllWhereOwnerId(int id)
        {
            return Repository.GetAllWhere(u => u.OwnerId == id);
        }
    }
}
