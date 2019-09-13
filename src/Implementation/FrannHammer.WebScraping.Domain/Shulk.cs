using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.Unique;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Shulk : WebCharacter
    {
        public Shulk()
            : base("Shulk",
                  uniqueScraperTypes: new List<Type>
                    {
                        typeof(MonadoArtsScraper)
                    },
                  potentialScrapingNames: new[] { "Shulk (Ground)", "Shulk (Air)" })
        {
            CssKey = "feelingit";
        }
    }
}