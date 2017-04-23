using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : ICharacterAttributeService
    {
        private readonly IRepository<ICharacterAttributeRow> _characterAttributeRepository;

        public DefaultCharacterAttributeService(IRepository<ICharacterAttributeRow> characterAttributeRepository)
        {
            Guard.VerifyObjectNotNull(characterAttributeRepository, nameof(characterAttributeRepository));
            _characterAttributeRepository = characterAttributeRepository;
        }

        public ICharacterAttributeRow Get(int id, string fields = "") => _characterAttributeRepository.Get(id);

        public IEnumerable<ICharacterAttributeRow> GetAll(string fields = "") => _characterAttributeRepository.GetAll();
        public ICharacterAttributeRow Add(ICharacterAttributeRow attribute)
        {
            Guard.VerifyObjectNotNull(attribute, nameof(attribute));
            return _characterAttributeRepository.Add(attribute);
        }

        public void AddMany(IEnumerable<ICharacterAttributeRow> attributes)
        {
            Guard.VerifyObjectNotNull(attributes, nameof(attributes));
            _characterAttributeRepository.AddMany(attributes);
        }
    }
}
