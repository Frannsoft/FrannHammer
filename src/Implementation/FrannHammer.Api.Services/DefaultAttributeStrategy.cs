using System;
using System.Reflection;
using FrannHammer.Utility;

namespace FrannHammer.Api.Services
{
    public class DefaultAttributeStrategy : IAttributeStrategy
    {
        public T GetAttributeFromProperty<T>(object instance, PropertyInfo propertyInfo)
            where T : Attribute
        {
            Guard.VerifyObjectNotNull(propertyInfo, nameof(propertyInfo));

            T returnedAttributeValue = default(T);

            var attributeValue = propertyInfo.GetCustomAttribute<T>();

            if (attributeValue != null)
            {
                returnedAttributeValue = attributeValue;
            }
            return returnedAttributeValue;
        }
    }
}
