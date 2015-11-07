using Kurogane.Data.RestApi.DTOs;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Controllers
{
    public class CharacterController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/{id}/character")]
        [HttpGet]
        public IEnumerable<CharacterDTO> Get()
        {
            return from chars in db.Characters
                   select EntityBusinessConverter<CharacterStat>.ConvertTo<CharacterDTO>(chars);
        }

        [Route("api/{id}/character")]
        [HttpGet]
        public CharacterDTO Get(int id)
        {
            if (id > 0)
            {
                var character = db.Characters.Find(id);

                return EntityBusinessConverter<CharacterStat>.ConvertTo<CharacterDTO>(character);
            }
            else
            {
                return null;
            }
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
                    db.Characters.Attach(stat);
                    db.Entry(stat).State = EntityState.Modified;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Unable to find value.");
                }
            }
            else
            {
                return BadRequest("Parameter null.");
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
                    db.Characters.Add(stat);
                    db.Entry(stat).State = EntityState.Added;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("api/{id}/character")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stat = db.Characters.Find(id);

            if (stat != null)
            {
                db.Characters.Remove(stat);
                db.Entry(stat).State = EntityState.Deleted;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
