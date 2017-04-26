using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class CharacterController : BaseApiController
    {
        private const string CharactersRouteKey = "characters";
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            Guard.VerifyObjectNotNull(characterService, nameof(characterService));
            _characterService = characterService;
        }

        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult GetCharacter(string id, string fields = "")
        {
            var character = _characterService.Get(id, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey)]
        public IHttpActionResult GetAllCharacters(string fields = "")
        {
            var characters = _characterService.GetAll(fields);
            return Result(characters);
        }
    }
}