﻿using System.Collections.Generic;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;

namespace FrannHammer.Services.Tests.Suts
{
    public class MoveSearchSut
    {
        public IEnumerable<dynamic> GetAll(ComplexMoveSearchModel searchModel, IApplicationDbContext context,
           IResultValidationService resultValidationService, string fields = "")
        {
            return new MetadataService(context, resultValidationService).GetAll<MoveDto>(searchModel, fields);
        }
    }
}