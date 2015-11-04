using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Characters;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Character")]
    public class CharacterController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        public IEnumerable<CharacterModel> Get()
        {
            return from chars in db.Characters.ToList()
                   select new CharacterModel()
                   {
                       FrameDataVersion = chars.FrameDataVersion,
                       FullUrl = chars.FullUrl,
                       Name = chars.Name,
                       OwnerId = chars.OwnerId
                   };
        }

        public CharacterModel Get(int id)
        {
            Character character = Character.FromId(id);
            return new CharacterModel()
            {
                FrameDataVersion = character.FrameDataVersion,
                FullUrl = character.FullUrl,
                Name = character.Name,
                OwnerId = character.OwnerId
            };
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
