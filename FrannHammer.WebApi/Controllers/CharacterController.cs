using System;
using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class CharacterController : BaseApiController
    {
        private const string CharactersRouteKey = "characters";
        private readonly ICharacterService _characterService;
        private readonly IMoveService _moveService;
        private readonly IMovementService _movementService;
        private readonly ICharacterAttributeRowService _attributeRowService;
        private readonly IDtoProvider _dtoProvider;

        public CharacterController(ICharacterService characterService, IMoveService moveService,
            IMovementService movementService, ICharacterAttributeRowService attributeRowService, IDtoProvider dtoProvider)
        {
            Guard.VerifyObjectNotNull(characterService, nameof(characterService));
            Guard.VerifyObjectNotNull(moveService, nameof(moveService));
            Guard.VerifyObjectNotNull(movementService, nameof(movementService));
            Guard.VerifyObjectNotNull(attributeRowService, nameof(attributeRowService));
            Guard.VerifyObjectNotNull(dtoProvider, nameof(dtoProvider));

            _characterService = characterService;
            _moveService = moveService;
            _movementService = movementService;
            _attributeRowService = attributeRowService;
            _dtoProvider = dtoProvider;
        }

        [Route(CharactersRouteKey + "/{id}")]
        public override IHttpActionResult GetById(string id, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleById(id, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey)]
        public override IHttpActionResult GetAll([FromUri]string fields = "")
        {
            var characters = _characterService.GetAll(fields);
            return Result(characters);
        }

        [Route(CharactersRouteKey + "/name/{name}")]
        public override IHttpActionResult GetAllWhereName(string name, [FromUri]string fields = "")
        {
            var character = _characterService.GetAllWhereName(name, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey + "/name/{name}/throws")]
        public IHttpActionResult GetAllThrowsForCharacterWhereName(string name, [FromUri]string fields = "")
        {
            var throws = _moveService.GetAllThrowsWhereCharacterNameIs(name, fields);
            return Result(throws);
        }

        [Route(CharactersRouteKey + "/name/{name}/moves")]
        public IHttpActionResult GetAllMovesForCharacterWhereName([FromUri] MoveFilterResourceQuery query)
        {
            var moves = _moveService.GetAllWhere(query);
            return Result(moves);
        }

        [Route(CharactersRouteKey + "/name/{name}/movements")]
        public IHttpActionResult GetAllMovementsForCharacterWhereName(string name, [FromUri] string fields = "")
        {
            var movements = _movementService.GetAllWhereCharacterNameIs(name, fields);
            return Result(movements);
        }

        [Route(CharactersRouteKey + "/name/{name}/details")]
        public IHttpActionResult GetDetailsForCharacterByName(string name, [FromUri] string fields = "")
        {
            var character =
                _characterService.GetSingleWhere(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if(character == null)
            { return NotFound() ;}

            //get movement, attributes and put into aggregate dto 
            var dto = _dtoProvider.CreateCharacterDetailsDto();

            var movements = _movementService.GetAllWhereCharacterNameIs(name, fields);
            var attributeRows = _attributeRowService.GetAllWhereCharacterNameIs(name, fields);

            dto.Metadata = character;
            dto.Movements = movements;
            dto.AttributeRows = attributeRows;

            return Result(dto);
        }
    }
}