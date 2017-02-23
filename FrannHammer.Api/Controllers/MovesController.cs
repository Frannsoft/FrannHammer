using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.ActionFilterAttributes;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services;
using StackExchange.Redis;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles server operations dealing with <see cref="Move"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class MovesController : BaseApiController
    {
        private const string MovesRouteKey = "moves";
        private readonly IMetadataService _metadataService;
        private readonly IConnectionMultiplexer _redisConnectionMultiplexer;

        /// <summary>
        /// Create a new <see cref="MovesController"/>.
        /// </summary>
        public MovesController(IMetadataService metadataService, IConnectionMultiplexer redisConnectionMultiplexer)
        {
            _metadataService = metadataService;
            _redisConnectionMultiplexer = redisConnectionMultiplexer;
        }

        /// <summary>
        /// Get all moves.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(MovesRouteKey)]
        public IHttpActionResult GetMoves([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Move, MoveDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Search through <see cref="Move"/>s based on their individual attributes.
        /// </summary>
        /// <param name="moveSearchModel"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/search")]
        [HttpPost]
        public IHttpActionResult MovesThatMeetCriteria(MoveSearchModel moveSearchModel, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<MoveSearchDto>(moveSearchModel, _redisConnectionMultiplexer, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get the <see cref="Hitbox"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/hitboxes")]
        public IHttpActionResult GetMoveHitboxData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<Hitbox, HitboxDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="Hitbox"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/hitboxes")]
        public IHttpActionResult GetMoveHitboxDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<Hitbox, HitboxDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="Autocancel"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(AutocancelDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/autocancel")]
        public IHttpActionResult GetMoveAutocancelData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<Autocancel, AutocancelDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="Autocancel"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(AutocancelDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/autocancel")]
        public IHttpActionResult GetMoveAutocancelDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<Autocancel, AutocancelDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="LandingLag"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(LandingLagDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/landinglag")]
        public IHttpActionResult GetMoveLandingLagData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<LandingLag, LandingLagDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="LandingLag"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(LandingLagDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/landinglag")]
        public IHttpActionResult GetMoveLandingLagDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<LandingLag, LandingLagDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="BaseKnockback"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseKnockbackDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/baseknockbacks")]
        public IHttpActionResult GetMoveBaseKnockbackData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<BaseKnockback, BaseKnockbackDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="BaseKnockback"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseKnockbackDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/baseknockbacks")]
        public IHttpActionResult GetMoveBaseKnockbackData(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<BaseKnockback, BaseKnockbackDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="SetKnockback"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SetKnockbackDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/setknockbacks")]
        public IHttpActionResult GetMoveSetKnockbackData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<SetKnockback, SetKnockbackDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="SetKnockback"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SetKnockbackDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/setknockbacks")]
        public IHttpActionResult GetMoveSetKnockbackDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<SetKnockback, SetKnockbackDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="Angle"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(AngleDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/angles")]
        public IHttpActionResult GetMoveAngleData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<Angle, AngleDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="Angle"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(AngleDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/angles")]
        public IHttpActionResult GetMoveAngleDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<Angle, AngleDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="FirstActionableFrame"/> data associated with this <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        [ResponseType(typeof(FirstActionableFrameDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/firstActionableFrames")]
        public IHttpActionResult GetMoveFirstActionableFrameData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<FirstActionableFrame, FirstActionableFrameDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="FirstActionableFrame"/> data associated with this <see cref="Move"/> by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        [ResponseType(typeof(FirstActionableFrameDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/firstActionableFrames")]
        public IHttpActionResult GetMoveFirstActionableFrameDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<FirstActionableFrame, FirstActionableFrameDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="BaseDamage"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseDamageDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/basedamages")]
        public IHttpActionResult GetMoveBaseDamageData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<BaseDamage, BaseDamageDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="BaseDamage"/> data associated with this move by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseDamageDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/basedamages")]
        public IHttpActionResult GetMoveBaseDamageDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<BaseDamage, BaseDamageDto>(name, fields);
        }

        /// <summary>
        /// Get the <see cref="KnockbackGrowth"/> data associated with this <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(KnockbackGrowthDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}/knockbackgrowths")]
        public IHttpActionResult GetMoveKnockbackGrowthData(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<KnockbackGrowth, KnockbackGrowthDto>(id, fields);
            return content != null ? Ok(content) : NotFound();
        }

        /// <summary>
        /// Get the <see cref="KnockbackGrowth"/> data associated with this <see cref="Move"/> by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(KnockbackGrowthDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}/knockbackgrowths")]
        public IHttpActionResult GetMoveKnockbackGrowthDataByName(string name, [FromUri] string fields = "")
        {
            return FindWithMovesUsingMoveName<KnockbackGrowth, KnockbackGrowthDto>(name, fields);
        }

        /// <summary>
        /// Get a specific <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult GetMove(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<Move, MoveDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Get all moves that have a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/name/{name}")]
        public IHttpActionResult GetMovesByName([FromUri] string name, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Move, MoveDto>(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase), fields, false);
            return Ok(content);
        }

        /// <summary>
        /// Update a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(IHttpActionResult))]
        [ValidateModel]
        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult PutMove(int id, MoveDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Move, MoveDto>(id, dto);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Move"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "")]
        public IHttpActionResult PostMove(MoveDto dto)
        {
            var newDto = _metadataService.Add<Move, MoveDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = MovesRouteKey + "", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Move))]
        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult DeleteMove(int id)
        {
            _metadataService.Delete<Move>(id);
            return StatusCode(HttpStatusCode.OK);
        }

        private IHttpActionResult FindWithMovesUsingMoveName<T, TDto>(string moveName, string fields)
            where T : class, IMoveIdEntity
            where TDto : class
        {
            var ids = FindMovesUsingName(moveName).Select(m => m.Id).ToList();

            if (ids.Count > 0)
            {
                var content = _metadataService.GetMultipleWithMoves<T, TDto>(ids, fields);
                return Ok(content);
            }
            return NotFound();
        }

        private IEnumerable<Move> FindMovesUsingName(string moveName)
        {
            return _metadataService.GetAllOfType<Move>().Where(c => c.Name.Equals(moveName, StringComparison.OrdinalIgnoreCase));
        }
    }
}