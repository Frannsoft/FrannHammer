using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Attributes
{
    public interface IAttributeProvider
    {
        IAttribute CreateAttribute();
        ICharacterAttributeRow CreateCharacterAttributeRow();
        IAttributeValueRowCollection CreateAttributeValueRowCollection();
        IAttributeValueRow CreateAttributeValueRow();
        IAttributeValue CreateAttributeValue();
    }
}
