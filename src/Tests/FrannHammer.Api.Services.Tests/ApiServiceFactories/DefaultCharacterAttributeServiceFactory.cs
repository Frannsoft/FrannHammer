using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Tests.ApiServiceFactories
{
    public sealed class DefaultCharacterAttributeServiceFactory : ApiServiceFactory<ICharacterAttributeRow>
    {
        public override ICrudService<ICharacterAttributeRow> CreateService(IRepository<ICharacterAttributeRow> repository)
        {
            return new DefaultCharacterAttributeService(repository, new DefaultCharacterAttributeNameProvider());
        }
    }
}