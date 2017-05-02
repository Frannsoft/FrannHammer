﻿using System;
using System.Collections.Generic;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.UniqueData
{
    public interface IUniqueDataScrapingService
    {
        IEnumerable<T> ScrapeUniqueData<T>(IUniqueDataProvider uniqueDataProvider, string xpath, Uri sourceUri);
    }
}