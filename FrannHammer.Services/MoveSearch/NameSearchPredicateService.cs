using System;
using FrannHammer.Models;

namespace FrannHammer.Services.MoveSearch
{
    public class NameSearchPredicateService
    {
        public Func<Move, bool> GetNamePredicate(string name)
        {

            if (!string.IsNullOrEmpty(name))
            {
                var trimmedName = name.Trim();
                return m => m.Name.IndexOf(trimmedName, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            else
            {
                return null; //all
            }
        }
    }
}
