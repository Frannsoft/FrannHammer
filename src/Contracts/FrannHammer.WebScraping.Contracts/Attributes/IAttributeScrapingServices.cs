using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Attributes
{
    public interface IAttributeScrapingServices : IWebServices
    {
        IAttribute CreateAttribute();
        ICharacterAttributeRow CreateCharacterAttributeRow();
    }
}