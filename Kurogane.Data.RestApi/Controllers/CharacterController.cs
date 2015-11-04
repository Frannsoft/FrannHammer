using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Characters;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/{id}/Character")]
    public class CharacterController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        public IEnumerable<CharacterDTO> Get()
        {
            return from chars in db.Characters.ToList()
                   select new CharacterDTO()
                   {
                       FrameDataVersion = chars.FrameDataVersion,
                       FullUrl = chars.FullUrl,
                       Name = chars.Name,
                       OwnerId = chars.OwnerId
                   };
        }

        public CharacterDTO Get(int id)
        {
            Character character = Character.FromId(id);
            return new CharacterDTO()
            {
                FrameDataVersion = character.FrameDataVersion,
                FullUrl = character.FullUrl,
                Name = character.Name,
                OwnerId = character.OwnerId
            };
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]CharacterDTO value)
        {
            if (value != null)
            {
                var stat = db.Characters.Find(value.Id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.Characters.Add(value);
                }
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return InternalServerError();
            }

        }
        [Route("{id}")]
        public IHttpActionResult Patch(int id, [FromBody]CharacterDTO value)
        {
            if (value != null)
            {
                var stat = db.Characters.Find(value.Id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.Characters.Add(value);
                }
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            var stat = db.Characters.Find(id);

            if (stat != null)
            {
                db.Characters.Remove(stat);
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
