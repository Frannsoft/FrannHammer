using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using System.Collections.Generic;
using FrannHammer.Domain;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultCharacterService2<T> : ICharacterService<T>
        where T : MongoModel
    {
        private readonly IRepository<T> _repository;

        public DefaultCharacterService2(IRepository<T> repository)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
            _repository = repository;
        }

        public T Get(int id, string fields = "")
        {
            var character = _repository.Get(id);
            return character;
        }

        public IEnumerable<T> GetAll(string fields = "")
        {
            return _repository.GetAll();
        }

        public void Add(T character)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            _repository.Add(character);
        }

        public void AddMany(IEnumerable<T> models)
        {
            Guard.VerifyObjectNotNull(models, nameof(models));
            _repository.AddMany(models);
        }
    }

    public class DefaultCharacterService : ICharacterService
    {
        private readonly IRepository<ICharacter> _repository;

        public DefaultCharacterService(IRepository<ICharacter> repository)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
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

        public void Add(ICharacter character)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            _repository.Add(character);
        }

        public void AddMany(IEnumerable<ICharacter> models)
        {
            Guard.VerifyObjectNotNull(models, nameof(models));
            _repository.AddMany(models);
        }
    }
}
