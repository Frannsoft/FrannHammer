using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping;
using System;
using System.Collections.Generic;

namespace FrannHammer.Api.Services
{
    public class DefaultMoveService : OwnerBasedApiService<IMove>, IMoveService
    {
        public DefaultMoveService(IRepository<IMove> repository, IGameParameterParserService gameParameterParserService)
            : base(repository, gameParameterParserService)
        {
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));

            var throwMoves = GetAllWhere(move => move.Owner.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                                 move.MoveType == MoveType.Throw.GetEnumDescription());

            return throwMoves;
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int ownerId)
        {
            var throwMoves = GetAllWhere(move => move.OwnerId == ownerId &&
                                                 move.MoveType == MoveType.Throw.GetEnumDescription());

            return throwMoves;
        }

        public IEnumerable<IMove> GetAllThrowsForCharacter(ICharacter character)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            Guard.VerifyStringIsNotNullOrEmpty(character.Name, nameof(character.Name));

            var throwMoves = GetAllWhere(move => move.Owner.Equals(character.Name) &&
                                                 move.MoveType == MoveType.Throw.GetEnumDescription());

            return throwMoves;
        }

        public IEnumerable<IMove> GetAllMovesForCharacter(ICharacter character)
        {
            var moves = GetAllWhere(move => move.OwnerId == character.OwnerId);
            return moves;
        }
    }
}
