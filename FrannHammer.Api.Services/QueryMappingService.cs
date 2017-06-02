using System.Collections.Generic;
using System.Reflection;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class QueryMappingService : IQueryMappingService
    {
        private readonly IAttributeStrategy _attributeStrategy;

        public QueryMappingService(IAttributeStrategy attributeStrategy)
        {
            Guard.VerifyObjectNotNull(attributeStrategy, nameof(attributeStrategy));
            _attributeStrategy = attributeStrategy;
        }

        public IDictionary<string, string> MapResourceQueryToDictionary(IMoveFilterResourceQuery query, BindingFlags flagsToLocateProperties)
        {
            Guard.VerifyObjectNotNull(query, nameof(query));

            var returnedDictionary = new Dictionary<string, string>();

            foreach (var property in query.GetType().GetProperties(flagsToLocateProperties))
            {
                var propertyValue = property.GetValue(query);

                if (propertyValue != null)
                {
                    var friendlyNameAttribute = _attributeStrategy.GetAttributeFromProperty<FriendlyNameAttribute>(query, property);
                    if (friendlyNameAttribute != null)
                    {
                        returnedDictionary.Add(friendlyNameAttribute.Name, propertyValue.ToString());
                    }
                }
            }

            return returnedDictionary;
        }
    }
}
