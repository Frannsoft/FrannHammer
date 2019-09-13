﻿using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Ridley : WebCharacter
    {
        public Ridley()
            : base("Ridley", potentialScrapingNames: "Not Lucas")
        {
            CssKey = "ridley";
        }
    }
}
