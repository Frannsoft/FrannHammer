using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Moves
{
    public interface IMoveScrapingServices : IWebServices
    {
        IMove CreateMove();
    }
}