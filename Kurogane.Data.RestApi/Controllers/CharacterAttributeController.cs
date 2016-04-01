using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using KuroganeHammer.Data.Core;
using static Kurogane.Data.RestApi.Models.RolesConstants;

namespace Kurogane.Data.RestApi.Controllers
{
    public class CharacterAttributeController : ApiController
    {
        private readonly ICharacterAttributeService _characterAttributeService;
        private readonly ICharacterStatService _characterStatService;

        public CharacterAttributeController(ICharacterAttributeService characterAttributeService, ICharacterStatService characterStatService)
        {
            _characterAttributeService = characterAttributeService;
            _characterStatService = characterStatService;
        }

        //[Authorize(Roles = Basic)]
        //[Route("characterattributes")]
        //[HttpGet]
        //public IHttpActionResult GetAttributes([FromUri] CharacterAttributes attributeType)
        //{
        //    var attributes = _characterAttributeService.GetCharacterAttributesByTypeId(attributeType)
        //        .GroupBy(a => a.OwnerId)
        //        .Select(g => new CharacterAttributeRowDTO(g.First().Rank, g.First().SmashAttributeTypeId, g.Key, g.ToDictionary(at => at.Name, at => at), _characterStatService));

        //    return Ok(attributes);
        //}

        [Authorize(Roles = Basic)]
        [Route("characterattributes/{id}")]
        [HttpGet]
        public IHttpActionResult GetAttributes(int id)
        {
            var attributes = _characterAttributeService.GetCharacterAttributesByTypeId(id)
                .GroupBy(a => a.OwnerId)
                .Select(g => new CharacterAttributeRowDTO(g.First().Rank, g.First().SmashAttributeTypeId, g.Key, g.ToDictionary(at => at.Name, at => at), _characterStatService));

            return Ok(attributes);
        }

        [Authorize(Roles = Admin)]
        [Route("characterattributes")]
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

            return CreatedAtRoute("DefaultApi", new { controller = "CharacterAttribute", id = value.Id }, value);
        }

        [Authorize(Roles = Admin)]
        [Route("characterattributes/{id}")]
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
                foundAttribute.SmashAttributeTypeId = value.SmashAttributeTypeId;
                foundAttribute.Name = value.Name;
                foundAttribute.Rank = value.Rank;
                foundAttribute.Value = value.Value;

                _characterAttributeService.UpdateCharacterAttribute(foundAttribute);
            }

            return Ok(value);
        }

        [Authorize(Roles = Admin)]
        [Route("characterattributes/{id}")]
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