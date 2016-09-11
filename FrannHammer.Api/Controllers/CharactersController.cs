using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.ActionFilterAttributes;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles server operations dealing with <see cref="Character"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class CharactersController : BaseApiController
    {
        private const string CharactersRouteKey = "Characters";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="CharactersController"/> to interact with the server.
        /// </summary>
        public CharactersController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all of the <see cref="CharacterDto"/> details.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterDto))]
        [ValidateModel]
        [Route(CharactersRouteKey)]
        public IHttpActionResult GetCharacters([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Character, CharacterDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="CharacterDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(CharacterDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult GetCharacter(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<Character, CharacterDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="CharacterDto"/>s details by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(CharacterDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/name/{name}")]
        public IHttpActionResult GetCharacterByName(string name, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<Character, CharacterDto>(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase), fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="Movement"/> data for a specific <see cref="Character"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(MovementDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/movements")]
        [HttpGet]
        public IHttpActionResult GetMovementsForCharacter(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Movement, MovementDto>(m => m.OwnerId == id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="Move"/> data for a specific <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/moves")]
        public IHttpActionResult GetMovesForCharacter(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Move, MoveDto>(m => m.OwnerId == id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="Throw"/>s for the specific <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ValidateModel]
        [ResponseType(typeof(ThrowDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/throws")]
        public IHttpActionResult GetThrowsForCharacter(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAllForOwnerId<Move, Throw, ThrowDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="CharacterAttribute"/>s of a specific <see cref="Character"/>.
        /// 
        /// These will be returned as <see cref="CharacterAttributeDto"/>s.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/characterattributes")]
        public IHttpActionResult GetCharacterAttributesForCharacter(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<CharacterAttribute, CharacterAttributeDto>(a => a.OwnerId == id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="CharacterAttribute"/>s of a specific <see cref="Character"/>.
        /// 
        /// These will be returned as <see cref="CharacterAttributeDto"/>s.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="smashAttributeTypeId"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/smashattributetypes/{smashAttributeTypeId}")]
        public IHttpActionResult GetCharacterAttributesForCharacter(int id, int smashAttributeTypeId, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<CharacterAttribute, CharacterAttributeDto>(c => c.OwnerId == id && c.SmashAttributeTypeId == smashAttributeTypeId, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="Angle"/>s of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(AngleDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/angles")]
        public IHttpActionResult GetCharacterMoveAngles(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAllForOwnerId<Move, Angle, AngleDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="Hitbox"/>es of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para>
        /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/hitboxes")]
        public IHttpActionResult GetCharacterMoveHitboxes(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAllForOwnerId<Move, Hitbox, HitboxDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="KnockbackGrowth"/>es of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para>
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(KnockbackGrowthDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/knockbackgrowths")]
        public IHttpActionResult GetCharacterMoveKnockbackGrowths(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAllForOwnerId<Move, KnockbackGrowth, KnockbackGrowthDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all the <see cref="BaseDamage"/>s of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// /// E.g., id,name to get back just the id and name.</para></param> 
        /// <returns></returns>
        [ResponseType(typeof(BaseDamageDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}/basedamages")]
        public IHttpActionResult GetCharacterMoveBaseDamages(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAllForOwnerId<Move, BaseDamage, BaseDamageDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Update a <see cref="CharacterDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult PutCharacter(int id, CharacterDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Character, CharacterDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="CharacterDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(CharacterDto))]
        [ValidateModel]
        [Route(CharactersRouteKey + "")]
        public IHttpActionResult PostCharacter(CharacterDto dto)
        {
            var newDto = _metadataService.Add<Character, CharacterDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = CharactersRouteKey + "", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult DeleteCharacter(int id)
        {
            _metadataService.Delete<Character>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}