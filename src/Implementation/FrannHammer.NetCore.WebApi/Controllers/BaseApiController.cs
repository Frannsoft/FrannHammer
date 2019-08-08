using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FrannHammer.NetCore.WebApi.Controllers
{
    public abstract class BaseApiController : Controller
    {
        protected IActionResult Result<T>(IEnumerable<T> content)
        {
            if (content == null)
            {
                return NotFound();
            }

            var response = new ObjectResult(content.ToList())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }

        protected IActionResult Result<T>(T content)
        {
            if (content == null)
            {
                return NotFound();
            }

            var response = new ObjectResult(content)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
            //var _enrichers = new Collection<IResponseEnricher>(new List<IResponseEnricher>
            //{
            //    new CharacterResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext),
            //    new CharacterAttributeNameResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext),
            //    new CharacterAttributeRowResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext),
            //    new ManyCharacterAttributeNameResourceEnricher(new CharacterAttributeNameResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)),
            //    new ManyCharacterAttributeRowResourceEnricher(new CharacterAttributeRowResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)),
            //    new ManyCharacterResourceEnricher(new CharacterResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)),
            //    new ManyMovementResourceEnricher(new MovementResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)),
            //    new ManyMoveResourceEnricher(new MoveResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)),
            //    new ManyUniqueDataResourceEnricher(new UniqueDataResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)),
            //    new UniqueDataResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext),
            //    new MovementResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext),
            //    new MoveResourceEnricher(new LinkProvider(), Mapper.Instance, linkGenerator, Response.HttpContext)

            //});

            //string contentAsString = JsonConvert.SerializeObject(content);
            //var modifiedResponseBody = _enrichers.First(e => e.CanEnrich(content))
            //             .Enrich(contentAsString);

            //var newResponse = new ObjectResult(modifiedResponseBody)
            //{
            //    StatusCode = (int)HttpStatusCode.OK
            //};
            //return newResponse;
        }
    }
}
