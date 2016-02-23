using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using System.Collections.Generic;

namespace Kurogane.Data.RestApi.Providers
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
        private readonly ICharacterStatRepository characterStatRepository;
        private readonly IUnitOfWork unitOfWork;

        public CharacterService(ICharacterStatRepository characterStatRepository, IUnitOfWork unitOfWork)
        {
            this.characterStatRepository = characterStatRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<CharacterStat> GetCharacters()
        {
            var characters = characterStatRepository.GetAll();
            return characters;
        }

        public CharacterStat GetCharacter(int id)
        {
            var character = characterStatRepository.GetCharacter(id);
            return character;
        }

        public void CreateCharacter(CharacterStat characterStat)
        {
            characterStatRepository.Add(characterStat);
            SaveCharacter();
        }

        public void UpdateCharacter(CharacterStat characterStat)
        {
            characterStatRepository.Update(characterStat);
            SaveCharacter();
        }

        public void SaveCharacter()
        {
            unitOfWork.Commit();
        }

        public void DeleteCharacter(CharacterStat characterStat)
        {
            characterStatRepository.Delete(characterStat);
            SaveCharacter();
        }
    }
}
