using KuroganeHammer.Model;
using System.Collections.Generic;
using System.Linq;

namespace KuroganeHammer.Data.Infrastructure
{
    public interface IMovementStatRepository : IRepository<MovementStat>
    {
        IEnumerable<MovementStat> GetMovementStatsByName(string name);
        IEnumerable<MovementStat> GetMovementStatsByCharacter(int characterId);
    }

    public class MovementStatRepository : RepositoryBase<MovementStat>, IMovementStatRepository
    {
        public MovementStatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public IEnumerable<MovementStat> GetMovementStatsByName(string movementName)
        {
            var movement = this.DbContext.MovementStats.Where(m => m.Name == movementName);

            return movement;
        }

        public IEnumerable<MovementStat> GetMovementStatsByCharacter(int characterId)
        {
            var movements = this.DbContext.MovementStats.Where(m => m.OwnerId == characterId);
            return movements;
        }

    }
}
