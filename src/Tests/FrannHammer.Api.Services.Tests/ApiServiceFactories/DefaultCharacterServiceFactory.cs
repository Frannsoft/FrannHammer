using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using Moq;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public sealed class DefaultCharacterServiceFactory : ApiServiceFactory<ICharacter>
    {
        public override ICrudService<ICharacter> CreateService(IRepository<ICharacter> repository)
        {
            var queryMappingService = new QueryMappingService(new Mock<IAttributeStrategy>().Object);
            return new DefaultCharacterService(repository,
                new DefaultDtoProvider(),
                new DefaultMovementService(new Mock<IRepository<IMovement>>().Object, queryMappingService),
                new DefaultCharacterAttributeService(new Mock<IRepository<ICharacterAttributeRow>>().Object, new DefaultCharacterAttributeNameProvider()),
                new DefaultMoveService(new Mock<IRepository<IMove>>().Object, queryMappingService),
                new DefaultUniqueDataService(new Mock<IRepository<IUniqueData>>().Object, queryMappingService));
        }
    }
}