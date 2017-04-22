using System.Collections.Generic;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultMovementService : IMovementService
    {
        private readonly IRepository<IMovement> _repository;

        public DefaultMovementService(IRepository<IMovement> repository)
        {
            Guard.VerifyObjectNotNull(repository, nameof(repository));
            _repository = repository;
        }

        public IMovement Get(int id, string fields = "")
        {
            return _repository.Get(id);
        }

        public IEnumerable<IMovement> GetAll(string fields = "")
        {
            return _repository.GetAll();
        }

        public IMovement Add(IMovement movement)
        {
            Guard.VerifyObjectNotNull(movement, nameof(movement));
            return _repository.Add(movement);
        }

        public void AddMany(IEnumerable<IMovement> movements)
        {
            Guard.VerifyObjectNotNull(movements, nameof(movements));
            _repository.AddMany(movements);
        }
    }
}
