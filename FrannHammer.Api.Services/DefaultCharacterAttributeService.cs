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

        public ICharacterAttributeRow GetById(string id, string fields = "") => _characterAttributeRowRepository.GetById(id);

        public ICharacterAttributeRow GetByName(string name, string fields = "")
            => _characterAttributeRowRepository.GetByName(name);

        public IEnumerable<ICharacterAttributeRow> GetAll(string fields = "") => _characterAttributeRowRepository.GetAll();
        public void Add(ICharacterAttributeRow attributeRow)
        {
            Guard.VerifyObjectNotNull(attributeRow, nameof(attributeRow));
            _characterAttributeRowRepository.Add(attributeRow);
        }

        public void AddMany(IEnumerable<ICharacterAttributeRow> attributeRows)
        {
            Guard.VerifyObjectNotNull(attributeRows, nameof(attributeRows));
            _characterAttributeRowRepository.AddMany(attributeRows);
        }
    }
}
