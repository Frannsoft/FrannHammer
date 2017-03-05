using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace FrannHammer.Core
{
    /// <summary>
    /// Supports building dynamic objects via <see cref="ExpandoObject"/> from an
    /// object and specified property values from that object.
    /// </summary>
    public class DtoBuilder
    {
        private readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _cachedPropertyInfo =
            new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> objects of type <typeparamref name="TDto"/> that follow the 
        /// specified class member Flags.
        /// 
        /// If the <see cref="PropertyInfo"/> data already exists in the cache this method just returns the 
        /// cached data.  Otherwise, it adds it and then returns the newly added data.
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        private Dictionary<string, PropertyInfo> FetchProperties<TDto>()
        {
            var type = typeof (TDto);
            Dictionary<string, PropertyInfo> typeProperties = null;

            if (_cachedPropertyInfo.TryGetValue(type, out typeProperties))
            {
                return typeProperties; //found existing property info don't bother reflecting
            }

            var properties = type.GetProperties(Flags);

            typeProperties = properties.ToDictionary(p => p.Name, p => p, StringComparer.InvariantCultureIgnoreCase);
            _cachedPropertyInfo.Add(type, typeProperties);

            return typeProperties;
        }

        /// <summary>
        /// Build up an <see cref="ExpandoObject"/> consisting of the specified field values
        /// as they exist on the passed in <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldsRaw"></param>
        /// <returns></returns>
        public IDictionary<string, object> Build<TEntity, TDto>(TEntity entity, string fieldsRaw)
        {
            Guard.VerifyObjectNotNull(entity, nameof(entity));

            var splitValues = SplitValues(fieldsRaw);

            return Assemble<TEntity, TDto>(entity, splitValues);
        }

        private IEnumerable<string> SplitValues(string fieldsRaw)
        {
            return fieldsRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IDictionary<string, object> Assemble<TEntity, TDto>(TEntity entity, IEnumerable<string> requestedFieldNames)
        {
            Guard.VerifyObjectNotNull(requestedFieldNames, nameof(requestedFieldNames));

            var fieldsNamesList = requestedFieldNames.Distinct().ToList();

            //get cached property information for this dto
            var typeProperties = FetchProperties<TDto>();

            //if no field names exist add all public instance ones for a 'default' dto object
            if (fieldsNamesList.Count == 0)
            {
                fieldsNamesList.AddRange(typeProperties.Keys);
            }

            var customDto = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var field in fieldsNamesList)
            {
                PropertyInfo propInfo;

                if (typeProperties.TryGetValue(field, out propInfo))
                {
                    //if null make empty so result is more web friendly
                    var value = propInfo.GetValue(entity) ?? string.Empty;

                    customDto.Add(propInfo.Name, value);
                }
            }

            return customDto;
        }
    }
}
