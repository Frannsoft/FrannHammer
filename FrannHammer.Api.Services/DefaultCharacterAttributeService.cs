using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : BaseApiService<ICharacterAttributeRow>, ICharacterAttributeRowService
    {
        public DefaultCharacterAttributeService(IRepository<ICharacterAttributeRow> characterAttributeRowRepository)
            : base(characterAttributeRowRepository)
        { }
    }
}
