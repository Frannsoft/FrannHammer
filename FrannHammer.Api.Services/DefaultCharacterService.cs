using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterService : BaseApiService<ICharacter>, ICharacterService
    {
        public DefaultCharacterService(IRepository<ICharacter> repository)
            : base(repository)
        { }

        public ICharacter GetSingleByOwnerId(int id, string fields = "")
        {
            return Repository.GetSingleWhere(c => c.OwnerId == id);
        }

        public ICharacter GetSingleByName(string name, string fields = "")
        {
            return Repository.GetSingleWhere(c => c.Name == name);
        }
    }
}
