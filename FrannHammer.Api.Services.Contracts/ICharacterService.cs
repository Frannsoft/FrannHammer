﻿using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface ICharacterService : ICrudService<ICharacter>
    {
        ICharacter GetSingleByOwnerId(int id, string fields = "");
        ICharacter GetSingleByName(string name, string fields = "");
        ICharacterDetailsDto GetCharacterDetails(string name, string fields = "");
        IEnumerable<IMovement> GetAllMovementsWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<IMovement> GetAllMovementsWhereCharacterOwnerIdIs(int id, string fields = "");
        ICharacterDetailsDto GetCharacterDetailsWhereCharacterOwnerIdIs(int id, string fields);
        IEnumerable<ICharacterAttributeRow> GetAllAttributesWhereCharacterOwnerIdIs(int id, string fields = "");
        IEnumerable<ParsedMove> GetDetailedMovesWhereCharacterOwnerIdIs(int id, string fields = "");
    }
}
