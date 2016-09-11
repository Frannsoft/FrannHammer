using System;
using System.Linq;
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
    /// Handles server operations dealing with <see cref="Move"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class MovesController : BaseApiController
    {
        private const string MovesRouteKey = "moves";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="MovesController"/>.
        /// </summary>
        public MovesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        //Too big to be useful?
        /// <summary>
        /// Get all moves.  Not sure if this is sticking around.
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
        /// Get all moves that have a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [ValidateModel]
        [Route(MovesRouteKey + "/byname")]
        public IHttpActionResult GetMovesByName([FromUri] string name, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Move, MoveDto>(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase), fields);
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
            //Db.Hitbox.SingleOrDefault(h => h.MoveId == id)
            var content = _metadataService.GetWithMoves<Hitbox, HitboxDto>(id, fields);
            return Ok(content);
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
            //Db.Hitbox.SingleOrDefault(h => h.MoveId == id)
            var content = _metadataService.GetWithMoves<BaseKnockback, BaseKnockbackDto>(id, fields);
            return Ok(content);
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
            return Ok(content);
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
            return Ok(content);
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
            return Ok(content);
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
            return Ok(content);
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
    }
}