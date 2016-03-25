using System.Linq;
using System.Web.Http;
using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api")]
    public class CharacterAttributeController : ApiController
    {
        private readonly ICharacterAttributeService _characterAttributeService;

        public CharacterAttributeController(ICharacterAttributeService characterAttributeService)
        {
            _characterAttributeService = characterAttributeService;
        }

        [Authorize(Roles = "Basic")]
        [Route("attributes")]
        [HttpGet]
        public IHttpActionResult GetAttributes([FromUri] CharacterAttributes attributeType)
        {
            var attributes = from attribute in _characterAttributeService.GetCharacterAttributesByType(attributeType)
                             select new CharacterAttributeDTO(attribute);

            return Ok(attributes);
        }

        [Authorize(Roles = "Admin")]
        [Route("attributes")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CharacterAttribute value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundChar = _characterAttributeService.GetCharacterAttribute(value.Id);
            if (foundChar == null)
            {
                _characterAttributeService.CreateCharacterAttribute(value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [Route("attribute/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CharacterAttribute value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.OwnerId)
            {
                return BadRequest();
            }

            var foundAttribute = _characterAttributeService.GetCharacterAttribute(value.Id);

            if (foundAttribute != null)
            {
                foundAttribute.AttributeType = value.AttributeType;
                foundAttribute.Name = value.Name;
                foundAttribute.Rank = value.Rank;
                foundAttribute.Value = value.Value;

                _characterAttributeService.UpdateCharacterAttribute(foundAttribute);
            }

            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [Route("attributes/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var attribute = _characterAttributeService.GetCharacterAttribute(id);
            if (attribute == null)
            {
                return NotFound();
            }

            _characterAttributeService.DeleteCharacterAttribute(attribute);

            return Ok();
        }
    }
}