using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using Moq;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public sealed class MovementApiServiceFactory : ApiServiceFactory<IMovement>
    {
        public override ICrudService<IMovement> CreateService(IRepository<IMovement> repository)
        {
            return new DefaultMovementService(repository, new GameParameterParserService("Smash4"));
        }
    }
}