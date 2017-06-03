using System;
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

        public IDictionary<string, object> MapResourceQueryToDictionary(IMoveFilterResourceQuery query, BindingFlags flagsToLocateProperties)
        {
            Guard.VerifyObjectNotNull(query, nameof(query));

            var returnedDictionary = new Dictionary<string, object>();

            foreach (var property in query.GetType().GetProperties(flagsToLocateProperties))
            {
                var propertyValue = property.GetValue(query);

                if (propertyValue != null)
                {
                    bool includeInReturnedDictionary = true;

                    if (property.PropertyType.IsValueType)
                    {
                        //is the property value type the default value for its type?  
                        //If so, don't include it in the returned dictionary because
                        //it mess up the query by using an unexpected value.
                        if (Activator.CreateInstance(property.PropertyType).Equals(propertyValue))
                        { includeInReturnedDictionary = false; }
                    }

                    var friendlyNameAttribute = _attributeStrategy.GetAttributeFromProperty<FriendlyNameAttribute>(query, property);
                    if (friendlyNameAttribute != null && includeInReturnedDictionary)
                    {
                        returnedDictionary.Add(friendlyNameAttribute.Name, propertyValue);
                    }
                }
            }

            return returnedDictionary;
        }
    }
}
