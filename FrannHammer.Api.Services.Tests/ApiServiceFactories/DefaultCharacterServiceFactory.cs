using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public sealed class DefaultCharacterServiceFactory : ApiServiceFactory<ICharacter>
    {
        public override ICrudService<ICharacter> CreateService(IRepository<ICharacter> repository)
        {
            return new DefaultCharacterService(repository);
        }
    }
}