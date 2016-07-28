using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Core;
using FrannHammer.Models;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="BaseDamage"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class BaseDamagesController : BaseApiController
    {
        private const string BaseDamagesRouteKey = "BaseDamages";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="BaseDamagesController"/> to interact with the server.
        /// </summary>
        public BaseDamagesController(IMetadataService metadataService)
        {
            Guard.VerifyObjectNotNull(metadataService, nameof(metadataService));
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="BaseDamageDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseDamageDto))]
        [Route(BaseDamagesRouteKey)]
        public IHttpActionResult GetBaseDamages([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<BaseDamage, BaseDamageDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="BaseDamageDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseDamageDto))]
        [Route(BaseDamagesRouteKey + "/{id}")]
        public IHttpActionResult GetBaseDamage(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.GetWithMovesOnEntity<BaseDamage, BaseDamageDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="BaseDamageDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(BaseDamagesRouteKey + "/{id}")]
        public IHttpActionResult PutBaseDamage(int id, BaseDamageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<BaseDamage, BaseDamageDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="BaseDamageDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(BaseDamageDto))]
        [Route(BaseDamagesRouteKey)]
        public IHttpActionResult PostBaseDamage(BaseDamageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDto = _metadataService.Add<BaseDamage, BaseDamageDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "BaseDamages", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="BaseDamage"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(BaseDamagesRouteKey + "/{id}")]
        public IHttpActionResult DeleteBaseDamage(int id)
        {
            _metadataService.Delete<BaseDamage>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}