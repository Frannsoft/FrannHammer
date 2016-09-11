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
    /// Handles operations dealing with <see cref="Hitbox"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class HitboxesController : BaseApiController
    {
        private const string HitboxesRouteKey = "Hitboxes";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="HitboxesController"/> to interact with the server.
        /// </summary>
        public HitboxesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="HitboxDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(HitboxesRouteKey)]
        public IHttpActionResult GetHitboxes([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<Hitbox, HitboxDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="HitboxDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(HitboxesRouteKey + "/{id}")]
        public IHttpActionResult GetHitbox(int id, [FromUri] string fields = "")
        {
            //ensure that data from joined can persist on the object
            var content = _metadataService.GetWithMovesOnEntity<Hitbox, HitboxDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="Hitbox"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(HitboxesRouteKey + "/{id}")]
        public IHttpActionResult PutHitbox(int id, HitboxDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Hitbox, HitboxDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Hitbox"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(HitboxDto))]
        [ValidateModel]
        [Route(HitboxesRouteKey)]
        public IHttpActionResult PostHitbox(HitboxDto dto)
        {
            var newDto = _metadataService.Add<Hitbox, HitboxDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "Hitboxes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Hitbox"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(HitboxesRouteKey + "/{id}")]
        public IHttpActionResult DeleteHitbox(int id)
        {
            _metadataService.Delete<Hitbox>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}