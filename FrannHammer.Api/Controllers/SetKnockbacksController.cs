using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.ActionFilterAttributes;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="SetKnockback"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class SetKnockbacksController : BaseApiController
    {
        private const string SetKnockbacksRouteKey = "SetKnockbacks";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="SetKnockbacksController"/> to interact with the server.
        /// </summary>
        /// <param name="metadataService"></param>
        public SetKnockbacksController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="SetKnockbackDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SetKnockbackDto))]
        [ValidateModel]
        [Route(SetKnockbacksRouteKey)]
        public IHttpActionResult GetSetKnockbacks([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<SetKnockback, SetKnockbackDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="SetKnockbackDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SetKnockbackDto))]
        [ValidateModel]
        [Route(SetKnockbacksRouteKey + "/{id}")]
        public IHttpActionResult GetSetKnockback(int id, [FromUri] string fields = "")
        {
            //ensure that data from joined can persist on the object
            var content = _metadataService.GetWithMovesOnEntity<SetKnockback, SetKnockbackDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="SetKnockbackDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ResponseType(typeof(SetKnockbackDto))]
        [ValidateModel]
        [Route(SetKnockbacksRouteKey + "/{id}")]
        public IHttpActionResult PutSetKnockback(int id, SetKnockbackDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<SetKnockback, SetKnockbackDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="SetKnockbackDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(SetKnockbackDto))]
        [ValidateModel]
        [Route(SetKnockbacksRouteKey)]
        public IHttpActionResult PostSetKnockback(SetKnockbackDto dto)
        {
            var newDto = _metadataService.Add<SetKnockback, SetKnockbackDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "SetKnockbacks", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="SetKnockback"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(SetKnockbacksRouteKey + "/{id}")]
        public IHttpActionResult DeleteSetKnockback(int id)
        {
            _metadataService.Delete<SetKnockback>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}