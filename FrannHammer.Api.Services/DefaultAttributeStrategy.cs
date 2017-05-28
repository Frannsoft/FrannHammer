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
            else
            {
                throw new InvalidOperationException(
                    $"Unable to find an instance of {nameof(T)} on property {propertyInfo.Name}");
            }

            return returnedAttributeValue;
        }
    }
}
