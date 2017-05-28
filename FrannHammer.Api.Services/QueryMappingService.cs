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

        public IDictionary<string, string> MapResourceQueryToDictionary(IMoveFilterResourceQuery query)
        {
            Guard.VerifyObjectNotNull(query, nameof(query));

            var returnedDictionary = new Dictionary<string, string>();

            foreach (var property in query.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var friendlyNameAttribute = _attributeStrategy.GetAttributeFromProperty<FriendlyNameAttribute>(query, property);
                var propertyValue = property.GetValue(query);

                if (propertyValue != null)
                {
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
