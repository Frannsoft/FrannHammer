using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Services
{
    public static class ServiceExtensions
    {
        public static IEnumerable<T> SafeConcat<T>(this IEnumerable<T> currentItems, IEnumerable<T> newItems) 
            => newItems == null ? currentItems : currentItems.Concat(newItems);
    }
}
