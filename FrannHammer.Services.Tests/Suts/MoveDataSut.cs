using System.Collections.Generic;
using FrannHammer.Models;

namespace FrannHammer.Services.Tests.Suts
{
    public class MoveDataSut<T, TDto>
         where T : class, IMoveIdEntity
         where TDto : class
    {
        public dynamic GetWithMoves(int id, IApplicationDbContext context, string fields = "")
        {
            return new MetadataService(context).GetWithMoves<T, TDto>(id, fields);
        }

        public IEnumerable<dynamic> GetAllWithMoves(IApplicationDbContext context, string fields = "")
        {
            return new MetadataService(context).GetAllWithMoves<T, TDto>(fields);
        }
    }
}
