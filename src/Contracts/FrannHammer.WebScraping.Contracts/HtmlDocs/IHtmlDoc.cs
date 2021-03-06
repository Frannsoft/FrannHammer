﻿using System.Collections.Generic;

namespace FrannHammer.WebScraping.Contracts.HtmlDocs
{
    public interface IHtmlDoc
    {
        IEnumerable<string> SelectHtmlFromManyNodes(string xpath);
        string SelectSingleNodeHtml(string xpath);
        string GetAttributeFromSingleNavigable(string attributeName, string xpath);
    }
}
