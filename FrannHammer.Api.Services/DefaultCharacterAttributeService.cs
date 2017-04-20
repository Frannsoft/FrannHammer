using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : ICharacterAttributeService
    {
        private readonly IRepository<IAttribute> _characterAttributeRepository;

        public DefaultCharacterAttributeService(IRepository<IAttribute> characterAttributeRepository)
        {
            _characterAttributeRepository = characterAttributeRepository;
        }

        public IAttribute Get(int id, string fields = "") => _characterAttributeRepository.Get(id);

        public IEnumerable<IAttribute> GetAll(string fields = "") => _characterAttributeRepository.GetAll();
    }
}
