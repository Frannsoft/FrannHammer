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
            return new DefaultCharacterService(repository,
                new DefaultMovementService(new Mock<IRepository<IMovement>>().Object, new GameParameterParserService("Smash4")),
            new DefaultCharacterAttributeService(new Mock<IRepository<ICharacterAttributeRow>>().Object, new DefaultCharacterAttributeNameProvider(), new GameParameterParserService("Smash4")),
            new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new GameParameterParserService("Smash4")),
            new DefaultUniqueDataService(new Mock<IRepository<IUniqueData>>().Object, new GameParameterParserService("Smash4")),
            new GameParameterParserService("Smash4"));
        }
    }
}