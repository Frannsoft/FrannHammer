using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.Unique;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Olimar : WebCharacter
    {
        public Olimar()
            : base("Olimar",
                  uniqueScraperTypes: new List<Type>{
                      typeof(PikminScraper)
                  })
        {
            CssKey = "olimar";
        }
    }
}