using KuroganeHammer.Data.Core.Model.Characters;
using System.Collections.Generic;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Movement")]
    public class MovementController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            Character character = Character.FromId(id);
            return character.AsJson();
        }

        public void Post([FromBody]string value)
        {

        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
