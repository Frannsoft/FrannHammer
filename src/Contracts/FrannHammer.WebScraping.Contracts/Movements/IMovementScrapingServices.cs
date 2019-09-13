using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Movements
{
    public interface IMovementScrapingServices : IWebServices
    {
        IMovement CreateMovement();
    }
}