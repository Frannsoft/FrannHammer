using System.Collections.Generic;
using FrannHammer.Models;

namespace FrannHammer.Services.Tests.Suts
{
    public class MetadataSut<T, TDto>
       where T : class, IEntity
       where TDto : class
    {
        public IEnumerable<dynamic> GetAllWithMoves<TMoveEntity>(IApplicationDbContext context, string fields = "")
            where TMoveEntity : class, IMoveIdEntity
        {
            return new MetadataService(context).GetAllWithMoves<TMoveEntity, TDto>(fields);
        }

        public IEnumerable<dynamic> GetAll(IApplicationDbContext context, string fields = "")
        {
            return new MetadataService(context).GetAll<T, TDto>(fields);
        }

        public dynamic Get(int id, IApplicationDbContext context, string fields = "")
        {
            return new MetadataService(context).Get<T, TDto>(id, fields);
        }
    }
}
