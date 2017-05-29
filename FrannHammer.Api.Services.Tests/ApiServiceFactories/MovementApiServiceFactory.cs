using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public sealed class MovementApiServiceFactory : ApiServiceFactory<IMovement>
    {
        public override ICrudService<IMovement> CreateService(IRepository<IMovement> repository)
        {
            return new DefaultMovementService(repository);
        }
    }
}