using System.Collections.Generic;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Services
{
    public interface IMovementStatService
    {
        IEnumerable<MovementStat> GetMovementStats();
        IEnumerable<MovementStat> GetMovementStatsForCharacter(int id);
        IEnumerable<MovementStat> GetMovementStatsByName(string name);
        MovementStat GetMovementStat(int id);
        void SaveMovement();
        void CreateMovementStat(MovementStat movementStat);
        void UpdateMovementStat(MovementStat movementStat);
        void DeleteMovementStat(MovementStat movementStat);
    }

    public class MovementStatService : IMovementStatService
    {
        private readonly IMovementStatRepository _movementStatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MovementStatService(IMovementStatRepository movementStatRepository, IUnitOfWork unitOfWork)
        {
            _movementStatRepository = movementStatRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MovementStat> GetMovementStats()
        {
            var movements = _movementStatRepository.GetAll();
            return movements;
        }

        public MovementStat GetMovementStat(int id)
        {
            var movement = _movementStatRepository.GetById(id);
            return movement;
        }

        public IEnumerable<MovementStat> GetMovementStatsForCharacter(int id)
        {
            var movements = _movementStatRepository.GetMovementStatsByCharacter(id);
            return movements;
        }

        public void CreateMovementStat(MovementStat movementStat)
        {
            _movementStatRepository.Add(movementStat);
            SaveMovement();
        }

        public void SaveMovement()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<MovementStat> GetMovementStatsByName(string name)
        {
            var movements = _movementStatRepository.GetMovementStatsByName(name);
            return movements;
        }

        public void UpdateMovementStat(MovementStat movementStat)
        {
            _movementStatRepository.Update(movementStat);
            SaveMovement();
        }

        public void DeleteMovementStat(MovementStat movementStat)
        {
            _movementStatRepository.Delete(movementStat);
            SaveMovement();
        }
    }
}
