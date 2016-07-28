using System.Collections.Generic;
using System.Dynamic;

namespace FrannHammer.Core
{
    public static class CustomDtoExtensions
    {
        private static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Convert a <see cref="IDictionary{String, Object}"/> to a dynamic object using <see cref="ExpandoObject"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static dynamic ToDynamicObject(this IDictionary<string, object> source)
        {
            ICollection<KeyValuePair<string, object>> retObject = new ExpandoObject();
            retObject.AddRange(source);
            return retObject;
        }
    }
}
