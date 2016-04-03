using System.Collections;
using NUnit.Framework;
using System.Linq;

namespace KuroganeHammer.Data.Api.Tests.Asserts
{
    public class CollectionPropertyAssert
    {
        /// <summary>
        /// Asserts that every property in the passed in collection (only checks PUBLIC instance properties)
        /// is not null.
        /// </summary>
        /// <param name="collection"></param>
        public static void AllPropertiesNotNullInCollection(IEnumerable collection, params string[] exclusions)
        {
            foreach (var item in collection)
            {
                var props = item.GetType().GetProperties();

                foreach (var prop in props)
                {
                    if (!exclusions.Contains(prop.Name))
                    {
                        Assert.True(prop.GetValue(item) != null, $"{prop.Name} on {item} object was null.");
                    }
                }
            }
        }
    }
}
