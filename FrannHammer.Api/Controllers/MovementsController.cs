using System;
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
    /// Handles server operations dealing with <see cref="Movement"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class MovementsController : BaseApiController
    {
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="MovementsController"/>.
        /// </summary>
        public MovementsController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        //Query too big to be useful.
        /// <summary>
        /// Get all movement data.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        [ResponseType(typeof(MovementDto))]
        [ValidateModel]
        [Route("movements", Name = "GetAllMovements")]
        public IHttpActionResult GetMovements([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Movement, MovementDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get all of the <see cref="Movement"/> data that is a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(MovementDto))]
        [ValidateModel]
        [Route("movements/name/{name}", Name = "GetMovementsByName")]
        public IHttpActionResult GetMovementsByName([FromUri] string name, [FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<Movement, MovementDto>(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase), fields, false);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(MovementDto))]
        [ValidateModel]
        [Route("movements/{id}")]
        public IHttpActionResult GetMovement(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<Movement, MovementDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a specific <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Route("movements/{id}")]
        public IHttpActionResult PutMovement(int id, MovementDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<Movement, MovementDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Movement"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(MovementDto))]
        [ValidateModel]
        [Route("movements")]
        public IHttpActionResult PostMovement(MovementDto dto)
        {
            var newDto = _metadataService.Add<Movement, MovementDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "Movements", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route("movements/{id}")]
        public IHttpActionResult DeleteMovement(int id)
        {
            _metadataService.Delete<Movement>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}