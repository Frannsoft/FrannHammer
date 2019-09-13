using System;
using System.Reflection;

namespace FrannHammer.Api.Services
{
    /// <summary>
    /// The strategy for attributes
    /// </summary>
    public interface IAttributeStrategy
    {
        /// <summary>
        /// Get the attribute from property info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        T GetAttributeFromProperty<T>(object instance, PropertyInfo propertyInfo)
            where T : Attribute;
    }
}