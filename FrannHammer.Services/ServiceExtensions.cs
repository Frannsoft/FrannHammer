using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Services
{
    public static class ServiceExtensions
    {
        public static IList<T> SafeWhere<T>(this IList<T> currentItems, IEnumerable<T> newItems)
        {
            if (newItems == null)
            { return currentItems; }

            var matches = currentItems.Count == 0 ?
                                newItems :
                                newItems.Where(currentItems.Contains);

            return matches.ToList();
        }

    }
}
