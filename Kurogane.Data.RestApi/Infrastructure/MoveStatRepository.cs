using Kurogane.Data.RestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public interface IMoveStatRepository : IRepository<MoveStat>
    {
        List<MoveStat> GetMoveStatByName(string name);
        List<MoveStat> GetMoveStatByType(MoveType moveType);
        List<MoveStat> GetMoveStatsByCharacter(int id);
    }

    public class MoveStatRepository : RepositoryBase<MoveStat>, IMoveStatRepository
    {
        public MoveStatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public List<MoveStat> GetMoveStatByName(string name)
        {
            List<MoveStat> moves = null;

            using (var context = new Sm4shEntities())
            {
                moves = context.Moves.Where(m => m.Name == name).ToList();
            }
            return moves;
        }


        public List<MoveStat> GetMoveStatByType(MoveType moveType)
        {
            List<MoveStat> moves = null;

            using (var context = new Sm4shEntities())
            {
                moves = context.Moves.Where(m => m.Type == moveType).ToList();
            }
            return moves;
        }


        public List<MoveStat> GetMoveStatsByCharacter(int id)
        {
            List<MoveStat> moves = null;

            using (var context = new Sm4shEntities())
            {
                moves = context.Moves.Where(m => m.OwnerId == id).ToList();
            }
            return moves;
        }
    }
}
