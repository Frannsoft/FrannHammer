using System.Collections.Generic;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Services
{
    public interface ICharacterStatService
    {
        IEnumerable<CharacterStat> GetCharacters();
        CharacterStat GetCharacter(int id);
        void CreateCharacter(CharacterStat characterStat);
        void UpdateCharacter(CharacterStat characterStat);
        void SaveCharacter();
        void DeleteCharacter(CharacterStat characterStat);
    }

    public class CharacterService : ICharacterStatService
    {
        private readonly ICharacterStatRepository _characterStatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CharacterService(ICharacterStatRepository characterStatRepository, IUnitOfWork unitOfWork)
        {
            _characterStatRepository = characterStatRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CharacterStat> GetCharacters()
        {
            var characters = _characterStatRepository.GetAll();
            return characters;
        }

        public CharacterStat GetCharacter(int id)
        {
            var character = _characterStatRepository.GetById(id);
            return character;
        }

        public void CreateCharacter(CharacterStat characterStat)
        {
            _characterStatRepository.Add(characterStat);
            SaveCharacter();
        }

        public void UpdateCharacter(CharacterStat characterStat)
        {
            _characterStatRepository.Update(characterStat);
            SaveCharacter();
        }

        public void SaveCharacter()
        {
            _unitOfWork.Commit();
        }

        public void DeleteCharacter(CharacterStat characterStat)
        {
            _characterStatRepository.Delete(characterStat);
            SaveCharacter();
        }
    }
}
