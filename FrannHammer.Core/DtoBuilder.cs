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
    /// 
    /// A poor man's version of Facebook's graph api feature.
    /// </summary>
    public class DtoBuilder
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

        /// <summary>
        /// Build up an <see cref="ExpandoObject"/> consisting of the specified field values
        /// as they exist on the passed in <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldsRaw"></param>
        /// <returns></returns>
        public dynamic Build<TEntity>(TEntity entity, string fieldsRaw)
        {
            Guard.VerifyStringIsNotNullOrEmpty(fieldsRaw, nameof(fieldsRaw));
            Guard.VerifyObjectNotNull(entity, nameof(entity));

            var splitValues = SplitValues(fieldsRaw);

            return Assemble(entity, splitValues);
        }

        private IEnumerable<string> SplitValues(string fieldsRaw)
        {
            return fieldsRaw.Split(',');
        }

        private dynamic Assemble<TEntity>(TEntity entity, IEnumerable<string> requestedFieldNames)
        {
            Guard.VerifyObjectNotNull(requestedFieldNames, nameof(requestedFieldNames));

            var fieldsNamesList = requestedFieldNames.ToList();
            if (!fieldsNamesList.Any())
            { throw new ArgumentException("Must have at least one field specified! "); }

            var customDto = new Dictionary<string, object>();

            foreach (var field in fieldsNamesList)
            {
                var propInfo = entity.GetType().GetProperty(field, Flags);

                if (propInfo != null)
                {
                    var value = entity.GetType()
                        .GetProperty(field, Flags);

                    customDto.Add(propInfo.Name, value.GetValue(entity));
                }
            }

            dynamic resultObj = customDto.ToDynamicObject();
            return resultObj;
        }
    }
}
