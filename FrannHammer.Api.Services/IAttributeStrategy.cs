using System;
using System.Reflection;

namespace FrannHammer.Api.Services
{
    public interface IAttributeStrategy
    {
        T GetAttributeFromProperty<T>(object instance, PropertyInfo propertyInfo)
            where T : Attribute;
    }
}