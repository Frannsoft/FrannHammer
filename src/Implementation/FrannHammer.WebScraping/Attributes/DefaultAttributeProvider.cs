using System;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Attributes
{
    public class DefaultAttributeProvider : IAttributeProvider
    {
        public IAttribute CreateAttribute() => new CharacterAttribute();

        public IAttributeValueRowCollection CreateAttributeValueRowCollection()
        {
            throw new NotImplementedException();
        }

        public IAttributeValueRow CreateAttributeValueRow()
        {
            throw new NotImplementedException();
        }

        public IAttributeValue CreateAttributeValue()
        {
            throw new NotImplementedException();
        }
    }
}
