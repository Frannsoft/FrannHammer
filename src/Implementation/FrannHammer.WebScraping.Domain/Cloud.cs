using System;
using System.Collections.Generic;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Cloud : WebCharacter
    {
        public Cloud()
            : base("Cloud", uniqueScraperTypes: new List<Type>
            {
                typeof(LimitBreakScraper)
            })
        {
        }
    }
}