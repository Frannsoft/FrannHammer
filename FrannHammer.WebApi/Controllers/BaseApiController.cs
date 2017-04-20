using System.Web.Http;

namespace FrannHammer.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult Result<T>(T content)
        {
            if (content == null)
            {
                return NotFound();
            }
            return Ok(content);
        }
    }
}