using System.Collections.Generic;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;

namespace FrannHammer.Services.Tests.Suts
{
    public class MoveDataSut<T, TDto>
         where T : class, IMoveIdEntity
         where TDto : class
    {
        public dynamic GetWithMoves(int id, IApplicationDbContext context, 
            IResultValidationService resultValidationService, string fields = "")
        {
            return new MetadataService(context, resultValidationService).GetWithMoves<T, TDto>(id, fields);
        }

        public IEnumerable<dynamic> GetAllWithMoves(IApplicationDbContext context,
            IResultValidationService resultValidationService, string fields = "")
        {
            return new MetadataService(context, resultValidationService).GetAllWithMoves<T, TDto>(fields);
        }
    }
}
