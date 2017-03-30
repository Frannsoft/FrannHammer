using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IMoveScrapingService
    {
        IEnumerable<IMove> GetGroundMoves(string xpath);
        IEnumerable<IMove> GetAerialMoves(string xpath);
        IEnumerable<IMove> GetSpecialMoves(string xpath);
    }
}
