using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.Api.Services
{
    public class DefaultUniqueDataService : OwnerBasedApiService<IUniqueData>, IUniqueDataService
    {
        public DefaultUniqueDataService(IRepository<IUniqueData> repository,
            IGameParameterParserService gameParameterParserService)
            : base(repository, gameParameterParserService)
        {
        }

        public IEnumerable<IUniqueData> GetAllWhereOwnerId(int id)
        {
            return Repository.GetAllWhere(u => u.OwnerId == id);
        }
    }
}
