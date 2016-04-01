using System;
using System.Collections.Generic;
using System.Linq;
using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using Moq;

namespace Kurgane.Data.RestApi.Tests
{
    public class BaseTest
    {
        protected ICharacterStatService CharacterStatService;
        protected ICharacterStatRepository CharacterStatRepository;
        protected IMoveStatService MoveService;
        protected IMoveStatRepository MoveStatRepository;
        protected IMovementStatService MovementService;
        protected ICharacterAttributeService CharacterAttributeService;
        protected IMovementStatRepository MovementStatRepository;
        protected ICharacterAttributeRepository CharacterAttributeRepository;
        protected IUnitOfWork UnitOfWork;
        protected List<CharacterStat> RandomCharacters;

        public List<CharacterStat> SetupCharacters()
        {
            List<CharacterStat> _characters = new List<CharacterStat>
            {
                new CharacterStat
                {
                    ColorTheme = "#328490",
                    Description = "test description",
                    Id = 1,
                    MainImageUrl = "http://my.image.com/mi.png",
                    Name = "test character",
                    //OwnerId = 3,
                    Style = "test style",
                    ThumbnailUrl = "http://my.thumbnail.com/tn.png"
                },
                new CharacterStat
                {
                    ColorTheme = "#091231",
                    Description = "test desc #2",
                    Id = 2,
                    MainImageUrl = "http://woeihf.com/mm.png",
                    Name = "test character #2",
                    //OwnerId = 25,
                    Style = "test style #2",
                    ThumbnailUrl = "http://wiehofwhhq.com/pp.png"
                }
            };

            return _characters;
        }

        public ICharacterStatRepository SetupCharacterStatRepository()
        {
            //init repository
            var repo = new Mock<ICharacterStatRepository>();

            //setup mocking behavior
            repo.Setup(r => r.GetAll()).Returns(RandomCharacters);

            //repo.Setup(r => r.GetCharacter(It.IsAny<int>()))
            //    .Returns(new Func<int, CharacterStat>(
            //        id => _randomCharacters.Find(c => c.OwnerId.Equals(id))));

            repo.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Func<int, CharacterStat>(
                    id => RandomCharacters.Find(c => c.Id.Equals(id))));

            repo.Setup(r => r.Add(It.IsAny<CharacterStat>()))
                .Callback(new Action<CharacterStat>(newCharacterStat =>
                {
                    dynamic maxCharacterStatId = RandomCharacters.Last().Id;
                    dynamic nextCharacterStatId = maxCharacterStatId + 1;
                    newCharacterStat.Id = nextCharacterStatId;
                    RandomCharacters.Add(newCharacterStat);
                }));

            repo.Setup(r => r.Update(It.IsAny<CharacterStat>()))
                .Callback(new Action<CharacterStat>(x =>
                {
                    var oldCharacterStat = RandomCharacters.Find(c => c.Id == x.Id);
                    oldCharacterStat = x;
                    oldCharacterStat.LastModified = DateTime.Now;
                }));

            repo.Setup(r => r.Delete(It.IsAny<CharacterStat>()))
                .Callback(new Action<CharacterStat>(x =>
                {
                    var _characterStatToRemove = RandomCharacters.Find(c => c.Id == x.Id);
                    if (_characterStatToRemove != null)
                    {
                        RandomCharacters.Remove(_characterStatToRemove);
                    }
                }));

            return repo.Object;
        }

        public IMoveStatRepository SetupMoveStatRepository()
        {
            var repo = new Mock<IMoveStatRepository>();

            return repo.Object;
        }

        public IMovementStatRepository SetupMovementStatRepository()
        {
            var repo = new Mock<IMovementStatRepository>();
            return repo.Object;
        }

        public ICharacterAttributeRepository SetupCharacterAttributeRepository()
        {
            var repo = new Mock<ICharacterAttributeRepository>();
            return repo.Object;
        }
       
    }

}
