using FrannHammer.Api.Services.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.Seeding.Contracts
{
    public interface ISeeder
    {
        void SeedCharacterData(WebCharacter character,
            ICharacterService characterService,
            IMovementService movementService,
            IMoveService moveService,
            ICharacterAttributeRowService characterAttributeService,
            IUniqueDataService uniqueDataService);
    }
}
