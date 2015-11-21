using KuroganeHammer.Data.Infrastructure;
using KuroganeHammer.Model;
using System.Collections.Generic;

namespace KuroganeHammer.Service
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
        private readonly IMovementStatRepository movementStatRepository;
        private readonly IUnitOfWork unitOfWork;

        public MovementStatService(IMovementStatRepository movementStatRepository, IUnitOfWork unitOfWork)
        {
            this.movementStatRepository = movementStatRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MovementStat> GetMovementStats()
        {
            var movements = movementStatRepository.GetAll();
            return movements;
        }

        public MovementStat GetMovementStat(int id)
        {
            var movement = movementStatRepository.GetById(id);
            return movement;
        }

        public IEnumerable<MovementStat> GetMovementStatsForCharacter(int id)
        {
            var movements = movementStatRepository.GetMovementStatsByCharacter(id);
            return movements;
        }

        public void CreateMovementStat(MovementStat movementStat)
        {
            movementStatRepository.Add(movementStat);
        }

        public void SaveMovement()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<MovementStat> GetMovementStatsByName(string name)
        {
            var movements = movementStatRepository.GetMovementStatsByName(name);
            return movements;
        }

        public void UpdateMovementStat(MovementStat movementStat)
        {
            movementStatRepository.Update(movementStat);
        }

        public void DeleteMovementStat(MovementStat movementStat)
        {
            movementStatRepository.Delete(movementStat);
        }
    }
}
