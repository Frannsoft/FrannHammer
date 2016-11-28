using System.Collections.Generic;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;

namespace FrannHammer.Services.Tests.Suts
{
    public class MetadataSut<T, TDto>
       where T : class, IEntity
       where TDto : class
    {
        public IEnumerable<dynamic> GetAllWithMoves<TMoveEntity>(IApplicationDbContext context, 
            IResultValidationService resultValidationService, string fields = "")
            where TMoveEntity : class, IMoveIdEntity
        {
            return new MetadataService(context, resultValidationService).GetAllWithMoves<TMoveEntity, TDto>(fields);
        }

        public IEnumerable<dynamic> GetAll(IApplicationDbContext context,
            IResultValidationService resultValidationService, string fields = "")
        {
            return new MetadataService(context, resultValidationService).GetAll<T, TDto>(fields);
        }

        public dynamic Get(int id, IApplicationDbContext context,
            IResultValidationService resultValidationService, string fields = "")
        {
            return new MetadataService(context, resultValidationService).Get<T, TDto>(id, fields);
        }
    }
}
