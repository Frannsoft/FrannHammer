using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IAttributeProvider
    {
        IAttribute CreateAttribute();
        IAttributeValueRowCollection CreateAttributeValueRowCollection();
        IAttributeValueRow CreateAttributeValueRow();
        IAttributeValue CreateAttributeValue();
    }
}
