﻿using Kurogane.Data.RestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public interface IMovementStatRepository : IRepository<MovementStat>
    {
        List<MovementStat> GetMovementStatsByName(string name);
        List<MovementStat> GetMovementStatsByCharacter(int characterId);
    }

    public class MovementStatRepository : RepositoryBase<MovementStat>, IMovementStatRepository
    {
        public MovementStatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public List<MovementStat> GetMovementStatsByName(string movementName)
        {
            List<MovementStat> movement = null;

            using (var context = new Sm4shEntities())
            {
                movement = context.MovementStats.Where(m => m.Name == movementName).ToList();
            }
            return movement;
        }

        public List<MovementStat> GetMovementStatsByCharacter(int characterId)
        {
            List<MovementStat> movements = null;

            using(var context = new Sm4shEntities())
            {
                movements = context.MovementStats.Where(m => m.OwnerId == characterId).ToList();  
            }
            return movements;
        }

    }
}