using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Characters;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using KuroganeHammer.Data.Core.Model.Stats;

namespace Kurogane.Data.RestApi.Controllers
{
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

        [Route("api/{id}/character")]
        [HttpGet]
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

        [Route("api/{id}/character")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CharacterDTO value)
        {
            if (value != null)
            {
                var stat = db.Characters.Find(value.Id);

                if (stat != null)
                {
                    stat.FrameDataVersion = value.FrameDataVersion;
                    stat.FullUrl = value.FullUrl;
                    stat.Name = value.Name;
                    stat.OwnerId = value.OwnerId;
                }
                else
                {
                    stat = new CharacterStat()
                    {
                        FrameDataVersion = value.FrameDataVersion,
                        FullUrl = value.FullUrl,
                        Name = value.Name,
                        OwnerId = value.OwnerId
                    };

                    db.Characters.Add(stat);
                }
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return InternalServerError();
            }

        }

        [Route("api/{id}/character")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CharacterDTO value)
        {
            if (value != null)
            {
                var stat = db.Characters.Find(id);

                if (stat != null)
                {
                    stat.FrameDataVersion = value.FrameDataVersion;
                    stat.FullUrl = value.FullUrl;
                    stat.Name = value.Name;
                    stat.OwnerId = value.OwnerId;

                    db.SaveChanges();
                }
                else
                {
                    return InternalServerError();
                }
               
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
