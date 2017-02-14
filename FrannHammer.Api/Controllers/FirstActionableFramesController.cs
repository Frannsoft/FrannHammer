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
    /// Handles operation dealing with <see cref="FirstActionableFrame"/>
    /// </summary>
    [RoutePrefix("api")]
    public class FirstActionableFramesController : BaseApiController
    {
        private const string FirstActionableFramesRouteKey = "FirstActionableFrames";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="FirstActionableFramesController"/> to interact with the server.
        /// </summary>
        /// <param name="metadataService"></param>
        public FirstActionableFramesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="FirstActionableFrameDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(FirstActionableFrameDto))]
        [ValidateModel]
        [Route(FirstActionableFramesRouteKey)]
        public IHttpActionResult GetFirstActionableFrames([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<FirstActionableFrame, FirstActionableFrameDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="FirstActionableFrameDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(FirstActionableFrameDto))]
        [ValidateModel]
        [Route(FirstActionableFramesRouteKey + "/{id}")]
        public IHttpActionResult GetFirstActionableFrame(int id, [FromUri] string fields = "")
        {
            //ensure that data from joined can persist on the object
            var content = _metadataService.GetWithMovesOnEntity<FirstActionableFrame, FirstActionableFrameDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="FirstActionableFrameDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ValidateModel]
        [Route(FirstActionableFramesRouteKey + "/{id}")]
        public IHttpActionResult PutFirstActionableFrame(int id, FirstActionableFrameDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<FirstActionableFrame, FirstActionableFrameDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="FirstActionableFrameDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(FirstActionableFrameDto))]
        [ValidateModel]
        [Route(FirstActionableFramesRouteKey)]
        public IHttpActionResult PostFirstActionableFrame(FirstActionableFrameDto dto)
        {
            var newDto = _metadataService.Add<FirstActionableFrame, FirstActionableFrameDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "FirstActionableFrames", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="FirstActionableFrame"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(FirstActionableFramesRouteKey + "/{id}")]
        public IHttpActionResult DeleteFirstActionableFrame(int id)
        {
            _metadataService.Delete<FirstActionableFrame>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}