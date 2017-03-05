using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.ActionFilterAttributes;
using FrannHammer.Api.Models;
using FrannHammer.Core;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="Throw"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class ThrowsController : BaseApiController
    {
        private const string ThrowsRouteKey = "Throws";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="ThrowsController"/> to interact with the server.
        /// </summary>
        public ThrowsController(IMetadataService metadataService)
        {
            Guard.VerifyObjectNotNull(metadataService, nameof(metadataService));
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="ThrowDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowDto))]
        [ValidateModel]
        [Route(ThrowsRouteKey)]
        public IHttpActionResult GetThrows([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Throw, ThrowDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="ThrowDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowDto))]
        [ValidateModel]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult GetThrow(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<Throw, ThrowDto>(id, fields);

            return ReturnResponse(content);
        }

        /// <summary>
        /// Update a <see cref="ThrowDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowDto))]
        [ValidateModel]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult PutThrow(int id, ThrowDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Throw, ThrowDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="ThrowDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(ThrowDto))]
        [ValidateModel]
        [Route(ThrowsRouteKey)]
        public IHttpActionResult PostThrow(ThrowDto dto)
        {
            var newDto = _metadataService.Add<Throw, ThrowDto>(dto);

            return CreatedAtRoute("DefaultApi", new { controller = "Throws", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Throw"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult DeleteThrow(int id)
        {
            _metadataService.Delete<Throw>(id);

            return StatusCode(HttpStatusCode.OK);
        }
    }
}