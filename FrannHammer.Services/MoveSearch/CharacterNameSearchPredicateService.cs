using System;
using FrannHammer.Models;

namespace FrannHammer.Services.MoveSearch
{
    public class CharacterNameSearchPredicateService : SearchPredicateService
    {
        public Func<Character, bool> GetCharacterNamePredicate(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var trimmedName = name.Trim();
                return c => c.DisplayName.IndexOf(trimmedName, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            else
            {
                return c => c.DisplayName.Length > 0; //all
            }
        }
    }
}
