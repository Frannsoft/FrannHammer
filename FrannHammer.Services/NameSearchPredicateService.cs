using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class NameSearchPredicateService
    {
        public Func<Move, bool> GetNameDelegate(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return m => m.Name.Contains(name);
            }
            else
            {
                return m => m.Name.Length > 0; //all
            }
        }
    }
}
