﻿using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.Api.Services.Contracts
{
    public interface IMoveService : ICrudService<IMove>
    {
        IEnumerable<IDictionary<string, string>> GetAllPropertyDataWhereName(string name, string property,
            string fields = "");

        IDictionary<string, string> GetPropertyDataWhereId(string id, string property, string fields = "");
        IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<IMove> GetAllWhereCharacterNameIs(string name, string fields = "");
        IEnumerable<IMove> GetAllWhere(IMoveFilterResourceQuery query, string fields = "");

        IEnumerable<ParsedMove> GetAllMovePropertyDataForCharacter(ICharacter character, string fields = "");

        IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int ownerId, string fields = "");
        IEnumerable<IMove> GetAllWhereCharacterOwnerIdIs(int id, string fields = "");


        //new
        IEnumerable<IMove> GetAllThrowsForCharacter(ICharacter character, string fields = "");
        IEnumerable<IMove> GetAllMovesForCharacter(ICharacter character, string fields = "");

    }
}
