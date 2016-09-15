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
    /// Handles operations dealing with <see cref="BaseKnockback"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class BaseKnockbacksController : BaseApiController
    {
        private const string BaseKnockbacksRouteKey = "BaseKnockbacks";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="BaseKnockbacksController"/> to interact with the server.
        /// </summary>
        /// <param name="metadataService"></param>
        public BaseKnockbacksController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="BaseKnockbackDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseKnockbackDto))]
        [ValidateModel]
        [Route(BaseKnockbacksRouteKey)]
        public IHttpActionResult GetBaseKnockbacks([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<BaseKnockback, BaseKnockbackDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="BaseKnockbackDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseKnockbackDto))]
        [ValidateModel]
        [Route(BaseKnockbacksRouteKey + "/{id}")]
        public IHttpActionResult GetBaseKnockback(int id, [FromUri] string fields = "")
        {
            //ensure that data from joined can persist on the object
            var content = _metadataService.GetWithMovesOnEntity<BaseKnockback, BaseKnockbackDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="BaseKnockbackDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ValidateModel]
        [Route(BaseKnockbacksRouteKey + "/{id}")]
        public IHttpActionResult PutBaseKnockback(int id, BaseKnockbackDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<BaseKnockback, BaseKnockbackDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="BaseKnockbackDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(BaseKnockbackDto))]
        [ValidateModel]
        [Route(BaseKnockbacksRouteKey)]
        public IHttpActionResult PostBaseKnockback(BaseKnockbackDto dto)
        {
            var newDto = _metadataService.Add<BaseKnockback, BaseKnockbackDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "BaseKnockbacks", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="BaseKnockback"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(BaseKnockbacksRouteKey + "/{id}")]
        public IHttpActionResult DeleteBaseKnockback(int id)
        {
            _metadataService.Delete<BaseKnockback>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}