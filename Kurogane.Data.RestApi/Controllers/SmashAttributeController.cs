using System.Web.Http;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using static Kurogane.Data.RestApi.Models.RolesConstants;


namespace Kurogane.Data.RestApi.Controllers
{
    public class SmashAttributeController : ApiController
    {
        private readonly ISmashAttributeService _smashAttributeService;

        public SmashAttributeController(ISmashAttributeService smashAttributeService)
        {
            _smashAttributeService = smashAttributeService;
        }

        [Authorize(Roles = Basic)]
        [Route("attributes")]
        [HttpGet]
        public IHttpActionResult GetAttributes()
        {
            var attributes = _smashAttributeService.GetAttributes();
            return Ok(attributes);
        }

        [Authorize(Roles = Basic)]
        [Route("attributes/{id}")]
        [HttpGet]
        public IHttpActionResult GetAttribute(int id)
        {
            var attribute = _smashAttributeService.GetAttribute(id);
            return Ok(attribute);
        }

        [Authorize(Roles = Admin)]
        [Route("attributes")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]SmashAttributeType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundChar = _smashAttributeService.GetAttribute(value.Id);
            if (foundChar == null)
            {
                _smashAttributeService.CreateSmashAttribute(value);
            }
            else
            {
                return BadRequest();
            }

            return CreatedAtRoute("DefaultApi", new { controller = "SmashAttribute", id = value.Id }, value);
        }
    }
}