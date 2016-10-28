using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class NameSearchPredicateService
    {
        public Func<Move, bool> GetNamePredicate(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return m => m.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            else
            {
                return m => m.Name.Length > 0; //all
            }
        }
    }
}
