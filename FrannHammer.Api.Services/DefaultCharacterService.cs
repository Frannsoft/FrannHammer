using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using System.Collections.Generic;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterService : ICharacterService
    {
        private readonly IRepository<ICharacter> _repository;

        public DefaultCharacterService(IRepository<ICharacter> repository)
        {
            _repository = repository;
        }

        public ICharacter Get(int id, string fields = "")
        {
            var character = _repository.Get(id);
            return character;
        }

        public IEnumerable<ICharacter> GetAll(string fields = "")
        {
            return _repository.GetAll();
        }
    }
}
