using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public class ContentResponseBuilder
    {
        public IDictionary<string, object> Build<T>(T entity, string fields)
            where T : IModel
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDictionary<string, object>> BuildMany<T>(IList<T> entities, string fields)
            where T : IModel
        {
            throw new NotImplementedException();
        }
    }
}
