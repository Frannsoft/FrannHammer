using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="Angle"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class AnglesController : BaseApiController
    {
        private const string AnglesRouteKey = "Angles";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="AnglesController"/> to interact with the server.
        /// </summary>
        /// <param name="metadataService"></param>
        public AnglesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="AngleDto"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(AngleDto))]
        [Route(AnglesRouteKey)]
        public IHttpActionResult GetAngles([FromUri] string fields = "")
        {
            var content = _metadataService.GetAllWithMoves<Angle, AngleDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="AngleDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(AngleDto))]
        [Route(AnglesRouteKey + "/{id}")]
        public IHttpActionResult GetAngle(int id, [FromUri] string fields = "")
        {
            //ensure that data from joined can persist on the object
            var content = _metadataService.GetWithMovesOnEntity<Angle, AngleDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="AngleDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(AnglesRouteKey + "/{id}")]
        public IHttpActionResult PutAngle(int id, AngleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Angle, AngleDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="AngleDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(AngleDto))]
        [Route(AnglesRouteKey)]
        public IHttpActionResult PostAngle(AngleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDto = _metadataService.Add<Angle, AngleDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "Angles", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Angle"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(AnglesRouteKey + "/{id}")]
        public IHttpActionResult DeleteAngle(int id)
        {
            _metadataService.Delete<Angle>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}