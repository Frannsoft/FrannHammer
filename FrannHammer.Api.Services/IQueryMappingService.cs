using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services
{
    public interface IQueryMappingService
    {
        IDictionary<string, string> MapResourceQueryToDictionary(IMoveFilterResourceQuery query);
    }
}