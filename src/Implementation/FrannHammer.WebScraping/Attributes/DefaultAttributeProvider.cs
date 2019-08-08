using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Domain.Contracts;
using System;

namespace FrannHammer.WebScraping.Attributes
{
    public class DefaultAttributeProvider : IAttributeProvider
    {
        private readonly IInstanceIdGenerator _instanceIdGenerator;

        public DefaultAttributeProvider(IInstanceIdGenerator instanceIdGenerator)
        {
            _instanceIdGenerator = instanceIdGenerator;
        }

        public IAttribute CreateAttribute() => new CharacterAttribute();

        public ICharacterAttributeRow CreateCharacterAttributeRow() => new CharacterAttributeRow() { InstanceId = _instanceIdGenerator.GenerateId() };

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
