﻿using FrannHammer.WebScraping.Contracts.Attributes;

namespace FrannHammer.WebScraping.Attributes
{
    public class RunSpeedScraper : AttributeScraper
    {
        public override string AttributeName => "RunSpeed";

        public RunSpeedScraper(IAttributeScrapingServices scrapingServices)
            : base("http://kuroganehammer.com/Smash4/DashSpeed", scrapingServices)
        { }
    }
}