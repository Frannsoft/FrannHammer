using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;

namespace FrannHammer.Api.Services
{
    public class DefaultMoveService : IMoveService
    {
        private readonly IRepository<IMove> _repository;

        public DefaultMoveService(IRepository<IMove> repository)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
            _repository = repository;
        }

        public IMove GetById(string id, string fields = "")
        {
            var move = _repository.GetById(id);
            return move;
        }

        public IMove GetByName(string name, string fields = "")
        {
            var move = _repository.GetByName(name);
            return move;
        }

        public IEnumerable<IMove> GetAll(string fields = "")
        {
            return _repository.GetAll();
        }

        public void Add(IMove model)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            _repository.Add(model);
        }

        public void AddMany(IEnumerable<IMove> moves)
        {
            Guard.VerifyObjectNotNull(moves, nameof(moves));
            _repository.AddMany(moves);
        }
    }
}
