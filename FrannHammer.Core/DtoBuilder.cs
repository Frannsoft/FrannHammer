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
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

        /// <summary>
        /// Build up an <see cref="ExpandoObject"/> consisting of the specified field values
        /// as they exist on the passed in <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldsRaw"></param>
        /// <returns></returns>
        public dynamic Build<TEntity, TDto>(TEntity entity, string fieldsRaw)
        {
            //Guard.VerifyStringIsNotNullOrEmpty(fieldsRaw, nameof(fieldsRaw));
            Guard.VerifyObjectNotNull(entity, nameof(entity));

            var splitValues = SplitValues(fieldsRaw);

            return Assemble<TEntity, TDto>(entity, splitValues);
        }

        private IEnumerable<string> SplitValues(string fieldsRaw)
        {
            return fieldsRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private dynamic Assemble<TEntity, TDto>(TEntity entity, IEnumerable<string> requestedFieldNames)
        {
            Guard.VerifyObjectNotNull(requestedFieldNames, nameof(requestedFieldNames));

            var fieldsNamesList = requestedFieldNames.ToList();

            //if no field names exist add all public instance ones for a 'default' dto object
            if (!fieldsNamesList.Any())
            {
                var props = typeof(TDto).GetProperties(Flags);
                fieldsNamesList.AddRange(props.Select(p => p.Name));
            }

            var customDto = new Dictionary<string, object>();

            foreach (var field in fieldsNamesList)
            {
                var propInfo = entity.GetType().GetProperty(field, Flags);

                if (propInfo != null)
                {
                    //if null make empty so result is more web friendly
                    var value = propInfo.GetValue(entity) ?? string.Empty;

                    customDto.Add(propInfo.Name, value);
                }
            }

            dynamic resultObj = customDto.ToDynamicObject();
            return resultObj;
        }
    }
}
