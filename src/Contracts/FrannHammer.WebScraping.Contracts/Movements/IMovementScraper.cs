using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Movements
{
    public interface IMovementScraper
    {
        IEnumerable<IMovement> GetMovementsForCharacter(WebCharacter character);
    }
}
