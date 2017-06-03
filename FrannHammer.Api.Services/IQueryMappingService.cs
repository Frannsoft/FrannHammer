using System.Collections.Generic;
using System.Reflection;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public interface IQueryMappingService
    {
        IDictionary<string, object> MapResourceQueryToDictionary(IMoveFilterResourceQuery query, BindingFlags flagsToLocateProperties);
    }
}