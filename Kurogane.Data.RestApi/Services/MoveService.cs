using System.Collections.Generic;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Services
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
        private readonly IMoveStatRepository _moveStatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MoveService(IMoveStatRepository moveStatRepository, IUnitOfWork unitOfWork)
        {
            _moveStatRepository = moveStatRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MoveStat> GetMovesByType(MoveType moveType)
        {
            var moves = _moveStatRepository.GetMoveStatByType(moveType);
            return moves;
        }

        public IEnumerable<MoveStat> GetMovesByName(string name)
        {
            var moves = _moveStatRepository.GetMoveStatByName(name);
            return moves;
        }

        public IEnumerable<MoveStat> GetMovesByCharacter(int id)
        {
            var moves = _moveStatRepository.GetMoveStatsByCharacter(id);
            return moves;
        }

        public void CreateMove(MoveStat moveStat)
        {
            _moveStatRepository.Add(moveStat);
            SaveMove();
        }

        public void SaveMove()
        {
            _unitOfWork.Commit();
        }

        public MoveStat GetMove(int id)
        {
            var move = _moveStatRepository.GetById(id);
            return move;
        }

        public void UpdateMove(MoveStat moveStat)
        {
            _moveStatRepository.Update(moveStat);
            SaveMove();
        }

        public void DeleteMove(MoveStat moveStat)
        {
            _moveStatRepository.Delete(moveStat);
            SaveMove();
        }
    }
}
