using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using System.Collections.Generic;

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
        IEnumerable<IMoveResource> EnrichManyMoves(IEnumerable<IMove> moves, bool expand = false);
        IEnumerable<dynamic> EnrichManyUniqueDatas(IEnumerable<IUniqueData> uniqueDatas);
        IMoveResource EnrichMove(IMove move, bool expand = false);
        MovementResource EnrichMovement(IMovement movement);
        dynamic EnrichUniqueData(IUniqueData uniqueData);
    }
}