using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public sealed class MoveApiServiceFactory : ApiServiceFactory<IMove>
    {
        public override ICrudService<IMove> CreateService(IRepository<IMove> repository)
        {
            return new DefaultMoveService(repository, new QueryMappingService(new DefaultAttributeStrategy()), string.Empty);
        }
    }
}