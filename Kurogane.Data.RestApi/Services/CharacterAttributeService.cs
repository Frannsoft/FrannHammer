using System.Collections.Generic;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Services
{
    public interface ICharacterAttributeService
    {
        CharacterAttribute GetCharacterAttribute(int id);
        IEnumerable<CharacterAttribute> GetCharacterAttributesByName(string name);
        IEnumerable<CharacterAttribute> GetCharacterAttributesByTypeId(int id);
        IEnumerable<CharacterAttribute> GetCharacterAttributesByCharacter(int ownerId);
        void CreateCharacterAttribute(CharacterAttribute attribute);
        void UpdateCharacterAttribute(CharacterAttribute attribute);
        void SaveCharacterAttribute();
        void DeleteCharacterAttribute(CharacterAttribute attribute);
    }

    public class CharacterAttributeService : ICharacterAttributeService
    {
        private readonly ICharacterAttributeRepository _characterAttributeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CharacterAttributeService(ICharacterAttributeRepository characterAttributeRepository, IUnitOfWork unitOfWork)
        {
            _characterAttributeRepository = characterAttributeRepository;
            _unitOfWork = unitOfWork;
        }

        public CharacterAttribute GetCharacterAttribute(int id)
        {
            var attribute = _characterAttributeRepository.GetAttribute(id);
            return attribute;
        }

        public IEnumerable<CharacterAttribute> GetCharacterAttributesByName(string name)
        {
            var attributes = _characterAttributeRepository.GetCharacterAttributesByName(name);
            return attributes;
        }

        public IEnumerable<CharacterAttribute> GetCharacterAttributesByTypeId(int id)
        {
            var attributes = _characterAttributeRepository.GetCharacterAttributesByAttributeTypeId(id);
            return attributes;
        }

        public IEnumerable<CharacterAttribute> GetCharacterAttributesByCharacter(int ownerId)
        {
            var attributes = _characterAttributeRepository.GetCharacterAttributesByCharacter(ownerId);
            return attributes;
        }

        public void CreateCharacterAttribute(CharacterAttribute attribute)
        {
            _characterAttributeRepository.Add(attribute);
            SaveCharacterAttribute();
        }

        public void UpdateCharacterAttribute(CharacterAttribute attribute)
        {
            _characterAttributeRepository.Update(attribute);
            SaveCharacterAttribute();
        }

        public void SaveCharacterAttribute()
        {
            _unitOfWork.Commit();
        }

        public void DeleteCharacterAttribute(CharacterAttribute attribute)
        {
            _characterAttributeRepository.Delete(attribute);
            SaveCharacterAttribute();
        }
    }
}