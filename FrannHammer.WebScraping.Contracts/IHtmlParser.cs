﻿using System.Collections.Generic;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IHtmlParser
    {
        IEnumerable<string> GetCollection(string xpath);
        string GetSingle(string xpath);
        string GetAttributeFromSingleNavigable(string attributeName, string xpath);
    }
}
