﻿using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class LedgeHopScraper : AttributeScraper
    {
        public override string AttributeName => "LedgeHop";

        public LedgeHopScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/LedgeHop", scrapingServices)
        { }
    }
}