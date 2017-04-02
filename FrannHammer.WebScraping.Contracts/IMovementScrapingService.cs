using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IMovementScrapingService
    {
        IEnumerable<IMovement> GetMovementsForCharacter(WebCharacter character);
    }
}
