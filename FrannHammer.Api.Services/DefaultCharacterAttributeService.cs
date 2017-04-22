using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : ICharacterAttributeService
    {
        private readonly IRepository<IAttribute> _characterAttributeRepository;

        public DefaultCharacterAttributeService(IRepository<IAttribute> characterAttributeRepository)
        {
            Guard.VerifyObjectNotNull(characterAttributeRepository, nameof(characterAttributeRepository));
            _characterAttributeRepository = characterAttributeRepository;
        }

        public IAttribute Get(int id, string fields = "") => _characterAttributeRepository.Get(id);

        public IEnumerable<IAttribute> GetAll(string fields = "") => _characterAttributeRepository.GetAll();
        public IAttribute Add(IAttribute attribute)
        {
            Guard.VerifyObjectNotNull(attribute, nameof(attribute));
            return _characterAttributeRepository.Add(attribute);
        }

        public void AddMany(IEnumerable<IAttribute> attributes)
        {
            Guard.VerifyObjectNotNull(attributes, nameof(attributes));
            _characterAttributeRepository.AddMany(attributes);
        }
    }
}
