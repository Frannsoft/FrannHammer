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
    /// Handles operations dealing with <see cref="Notation"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class NotationsController : BaseApiController
    {
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="NotationsController"/> to interact with the server.
        /// </summary>
        /// <param name="metadataService"></param>
        public NotationsController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="Notation"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(NotationDto))]
        [ValidateModel]
        [Route("notations")]
        public IHttpActionResult GetNotations([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Notation, NotationDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="Notation"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(NotationDto))]
        [ValidateModel]
        [Route("notations/{id}")]
        public IHttpActionResult GetNotation(int id, [FromUri] string fields = "")
        {
            var content =
                _metadataService.Get<Notation, NotationDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Update a <see cref="Notation"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Route("notations/{id}")]
        public IHttpActionResult PutNotation(int id, NotationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Notation, NotationDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Notation"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(NotationDto))]
        [ValidateModel]
        [Route("notations")]
        public IHttpActionResult PostNotation(NotationDto dto)
        {
            var newDto = _metadataService.Add<Notation, NotationDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "Notations", id = newDto.Id }, dto);
        }

        /// <summary>
        /// Delete a <see cref="Notation"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route("notations/{id}")]
        public IHttpActionResult DeleteNotation(int id)
        {
            _metadataService.Delete<Notation>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}