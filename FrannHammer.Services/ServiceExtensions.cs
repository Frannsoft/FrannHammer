using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Returns the <paramref name="currentItems"/> if <paramref name="newItems"/> are null.
        /// 
        /// If <paramref name="currentItems"/> is empty, returns <paramref name="newItems"/>.
        /// 
        /// If <paramref name="currentItems"/> has items, returns only items from <paramref name="newItems"/>
        /// that are present in <paramref name="currentItems"/>.  This is to filter out items that match
        /// only the current <see cref="Move"/> attribute type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentItems"></param>
        /// <param name="newItems"></param>
        /// <returns></returns>
        public static IList<T> SafeFilter<T>(this IList<T> currentItems, IEnumerable<T> newItems)
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
