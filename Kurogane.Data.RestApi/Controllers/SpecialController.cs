using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    public class SpecialController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/special")]
        [HttpGet]
        public IEnumerable<SpecialStatDTO> Get()
        {
            return from special in db.SpecialStats.ToList()
                   select EntityBusinessConverter<SpecialStat>.ConvertTo<SpecialStatDTO>(special);
        }

        [Route("api/characters/{id}/special")]
        [HttpGet]
        public IEnumerable<SpecialStatDTO> Get(int id)
        {
            return from special in db.SpecialStats.ToList()
                   where special.OwnerId == id
                   select EntityBusinessConverter<SpecialStat>.ConvertTo<SpecialStatDTO>(special);
        }

        [Route("api/{id}/special")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]SpecialStatDTO value)
        {
            if (value != null)
            {
                var stat = db.SpecialStats.Find(value.Id);

                if (stat != null)
                {
                    db.SpecialStats.Attach(stat);
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

        [Route("api/{id}/special")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SpecialStatDTO value)
        {
            if (value != null)
            {
                var stat = db.SpecialStats.Find(id);

                if (stat != null)
                {
                    db.SpecialStats.Add(stat);
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

        [Route("api/{id}/special")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stat = db.SpecialStats.Find(id);

            if (stat != null)
            {
                db.SpecialStats.Remove(stat);
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
