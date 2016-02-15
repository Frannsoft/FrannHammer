using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using System.Collections.Generic;

namespace Kurogane.Data.RestApi.Providers
{
    public interface IMoveStatService
    {
        IEnumerable<MoveStat> GetMovesByType(MoveType moveType);
        IEnumerable<MoveStat> GetMovesByName(string name);
        IEnumerable<MoveStat> GetMovesByCharacter(int id);
        MoveStat GetMove(int id);
        void CreateMove(MoveStat moveStat);
        void UpdateMove(MoveStat moveStat);
        void DeleteMove(MoveStat moveStat);
        void SaveMove();
    }

    public class MoveService : IMoveStatService
    {
        private readonly IMoveStatRepository moveStatRepository;
        private readonly IUnitOfWork unitOfWork;

        public MoveService(IMoveStatRepository moveStatRepository, IUnitOfWork unitOfWork)
        {
            this.moveStatRepository = moveStatRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MoveStat> GetMovesByType(MoveType moveType)
        {
            var moves = moveStatRepository.GetMoveStatByType(moveType);
            return moves;
        }

        public IEnumerable<MoveStat> GetMovesByName(string name)
        {
            var moves = moveStatRepository.GetMoveStatByName(name);
            return moves;
        }

        public IEnumerable<MoveStat> GetMovesByCharacter(int id)
        {
            var moves = moveStatRepository.GetMoveStatsByCharacter(id);
            return moves;
        }

        public void CreateMove(MoveStat moveStat)
        {
            moveStatRepository.Add(moveStat);
            SaveMove();
        }

        public void SaveMove()
        {
            unitOfWork.Commit();
        }

        public MoveStat GetMove(int id)
        {
            var move = moveStatRepository.GetById(id);
            return move;
        }

        public void UpdateMove(MoveStat moveStat)
        {
            moveStatRepository.Update(moveStat);
            SaveMove();
        }

        public void DeleteMove(MoveStat moveStat)
        {
            moveStatRepository.Delete(moveStat);
            SaveMove();
        }
    }
}
