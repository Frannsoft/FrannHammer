using Kurogane.Data.RestApi.Models;
using KuroganeHammer.Data.Core.Model.Characters;
using System.Collections.Generic;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Character")]
    public class CharacterController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        // GET: api/Character
        public IEnumerable<string> Get()
        {
            return new string[] { "value1111", "value2" };
        }

        // GET: api/Character/5
        public string Get(int id)
        {
            db.Characters.Add(new CharacterModel() { Name = "test" });
            db.SaveChanges();

            Character character = Character.FromId(id);
            return character.AsJson();
        }

        // POST: api/Character
        public IHttpActionResult Post([FromBody]CharacterModel value)
        {
            if (value != null)
            {
                db.Characters.Add(value);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return InternalServerError();
            }

        }

        // PUT: api/Character/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Character/5
        public void Delete(int id)
        {
        }
    }
}
