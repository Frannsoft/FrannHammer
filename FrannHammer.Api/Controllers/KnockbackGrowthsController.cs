using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="KnockbackGrowth"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class KnockbackGrowthsController : BaseApiController
    {
        private const string KnockbackGrowthsRouteKey = "KnockbackGrowths";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="KnockbackGrowthsController"/> to interact with the server.
        /// </summary>
        public KnockbackGrowthsController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="KnockbackGrowthDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(KnockbackGrowthDto))]
        [Route(KnockbackGrowthsRouteKey)]
        public IHttpActionResult GetKnockbackGrowths([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<KnockbackGrowth, KnockbackGrowthDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="KnockbackGrowthDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(KnockbackGrowthDto))]
        [Route(KnockbackGrowthsRouteKey + "/{id}")]
        public IHttpActionResult GetKnockbackGrowth(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMoves<KnockbackGrowth, KnockbackGrowthDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Update a <see cref="KnockbackGrowthDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(KnockbackGrowthsRouteKey + "/{id}")]
        public IHttpActionResult PutKnockbackGrowth(int id, KnockbackGrowthDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<KnockbackGrowth, KnockbackGrowthDto>(id, dto);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="KnockbackGrowthDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(KnockbackGrowthDto))]
        [Route(KnockbackGrowthsRouteKey)]
        public IHttpActionResult PostKnockbackGrowth(KnockbackGrowthDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDto = _metadataService.Add<KnockbackGrowth, KnockbackGrowthDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "KnockbackGrowths", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="KnockbackGrowth"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(KnockbackGrowthsRouteKey + "/{id}")]
        public IHttpActionResult DeleteKnockbackGrowth(int id)
        {
            _metadataService.Delete<KnockbackGrowth>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}