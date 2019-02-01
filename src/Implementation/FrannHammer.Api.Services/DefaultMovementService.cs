using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System.Collections.Generic;
using System.Reflection;

namespace FrannHammer.Api.Services
{
    public class DefaultMovementService : OwnerBasedApiService<IMovement>, IMovementService
    {
        private readonly IQueryMappingService _queryMappingService;

        public DefaultMovementService(IRepository<IMovement> repository, IQueryMappingService queryMappingService, IGameParameterParserService gameParameterParserService)
            : base(repository, gameParameterParserService)
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
