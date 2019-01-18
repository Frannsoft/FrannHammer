using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public interface IEnrichmentProvider
    {
        CharacterResource EnrichCharacter(ICharacter character);
        CharacterAttributeNameResource EnrichCharacterAttributeName(ICharacterAttributeName characterAttributeName);
        CharacterAttributeRowResource EnrichCharacterAttributeRow(ICharacterAttributeRow characterAttributeRow);
        IEnumerable<CharacterAttributeNameResource> EnrichManyCharacterAttributeNames(IEnumerable<ICharacterAttributeName> characterAttributeNames);
        IEnumerable<CharacterAttributeRowResource> EnrichManyCharacterAttributeRowResources(IEnumerable<ICharacterAttributeRow> characterAttributeRows);
        IEnumerable<CharacterResource> EnrichManyCharacters(IEnumerable<ICharacter> characters);
        IEnumerable<MovementResource> EnrichManyMovements(IEnumerable<IMovement> movements);
        IEnumerable<IMove> EnrichManyMoves(IEnumerable<IMove> moves);
        IEnumerable<UniqueDataResource> EnrichManyUniqueDatas(IEnumerable<IUniqueData> uniqueDatas);
        MoveResource EnrichMove(IMove move);
        MovementResource EnrichMovement(IMovement movement);
        UniqueDataResource EnrichUniqueData(IUniqueData uniqueData);
    }
}