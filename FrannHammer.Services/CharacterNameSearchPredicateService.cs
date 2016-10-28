using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class CharacterNameSearchPredicateService : SearchPredicateService
    {
        public Func<Character, bool> GetCharacterNamePredicate(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return c => c.DisplayName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            else
            {
                return c => c.DisplayName.Length > 0; //all
            }
        }
    }
}
