using System.Collections.Generic;
using System.Reflection;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultMovementService : OwnerBasedApiService<IMovement>, IMovementService
    {
        private readonly IQueryMappingService _queryMappingService;

        public DefaultMovementService(IRepository<IMovement> repository, IQueryMappingService queryMappingService, string game)
            : base(repository, game)
        {
            Guard.VerifyObjectNotNull(queryMappingService, nameof(queryMappingService));
            _queryMappingService = queryMappingService;
        }

        public IEnumerable<IMovement> GetAllWhere(IFilterResourceQuery query)
        {
            var queryFilterParameters = _queryMappingService.MapResourceQueryToDictionary(query,
                BindingFlags.Public | BindingFlags.Instance);

            return GetAllWhere(queryFilterParameters);
        }
    }
}
