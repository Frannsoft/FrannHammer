using KuroganeHammer.Model;
using System.Collections.Generic;
using System.Linq;

namespace KuroganeHammer.Data.Infrastructure
{
    public interface IMoveStatRepository : IRepository<MoveStat>
    {
        IEnumerable<MoveStat> GetMoveStatByName(string name);
        IEnumerable<MoveStat> GetMoveStatByType(MoveType moveType);
        IEnumerable<MoveStat> GetMoveStatsByCharacter(int id);
    }

    public class MoveStatRepository : RepositoryBase<MoveStat>, IMoveStatRepository
    {
        public MoveStatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public IEnumerable<MoveStat> GetMoveStatByName(string name)
        {
            var moves = this.DbContext.Moves.Where(m => m.Name == name);
            return moves;
        }


        public IEnumerable<MoveStat> GetMoveStatByType(MoveType moveType)
        {
            var moves = this.DbContext.Moves.Where(m => m.Type == moveType);
            return moves;
        }


        public IEnumerable<MoveStat> GetMoveStatsByCharacter(int id)
        {
            var moves = this.DbContext.Moves.Where(m => m.OwnerId == id);
            return moves;
        }
    }
}
