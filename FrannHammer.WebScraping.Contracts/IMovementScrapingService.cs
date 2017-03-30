using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IMovementScrapingService
    {
        IEnumerable<IMovement> GetMovements(string xpath);
    }
}
