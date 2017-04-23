using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterAttributeService : ICharacterAttributeRowService
    {
        private readonly IRepository<ICharacterAttributeRow> _characterAttributeRowRepository;

        public DefaultCharacterAttributeService(IRepository<ICharacterAttributeRow> characterAttributeRowRepository)
        {
            Guard.VerifyObjectNotNull(characterAttributeRowRepository, nameof(characterAttributeRowRepository));
            _characterAttributeRowRepository = characterAttributeRowRepository;
        }

        public ICharacterAttributeRow Get(int id, string fields = "") => _characterAttributeRowRepository.Get(id);

        public IEnumerable<ICharacterAttributeRow> GetAll(string fields = "") => _characterAttributeRowRepository.GetAll();
        public ICharacterAttributeRow Add(ICharacterAttributeRow attributeRow)
        {
            Guard.VerifyObjectNotNull(attributeRow, nameof(attributeRow));
            return _characterAttributeRowRepository.Add(attributeRow);
        }

        public void AddMany(IEnumerable<ICharacterAttributeRow> attributeRows)
        {
            Guard.VerifyObjectNotNull(attributeRows, nameof(attributeRows));
            _characterAttributeRowRepository.AddMany(attributeRows);
        }
    }
}
