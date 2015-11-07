using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Kurogane.Data.RestApi.DTOs;
using System.Data.Entity;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Controllers
{
    public class AerialController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/aerial")]
        [HttpGet]
        public IEnumerable<AerialStatDTO> Get()
        {
            return from aerial in db.AerialStats.ToList()
                   select EntityBusinessConverter<AerialStat>.ConvertTo<AerialStatDTO>(aerial);

            //return from aerial in db.AerialStats
            //       select new AerialStatDTO()
            //       {
            //           Angle = aerial.Angle,
            //           AutoCancel = aerial.Autocancel,
            //           BackKnockbackSetKnockback = aerial.BaseKnockbackSetKnockback,
            //           BaseDamage = aerial.BaseDamage,
            //           FirstActionableFrame = aerial.FirstActionableFrame,
            //           HitboxActive = aerial.HitboxActive,
            //           KnockbackGrowth = aerial.KnockbackGrowth,
            //           LandingLag = aerial.LandingLag,
            //           Name = aerial.Name,
            //           CharacterName = db.Characters.FirstOrDefault(s => s.OwnerId == aerial.OwnerId).Name,
            //           OwnerId = aerial.OwnerId,
            //           RawName = aerial.RawName,
            //           Id = aerial.Id
            //       };
        }

        [Route("api/characters/{id}/aerials")]
        [HttpGet]
        public IEnumerable<AerialStatDTO> Get(int id)
        {
            if (id > 0)
            {
                return from aerial in db.AerialStats.ToList()
                       where aerial.OwnerId == id
                       select EntityBusinessConverter<AerialStat>.ConvertTo<AerialStatDTO>(aerial);
            }
            else
            {
                return null;
            }
        }

        [Route("api/{id}/aerial")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]AerialStatDTO value)
        {
            if (value != null)
            {
                var stat = db.AerialStats.Find(value.Id);

                if (stat != null)
                {
                    db.AerialStats.Attach(stat);
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

        [Route("api/{id}/aerial")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]AerialStatDTO value)
        {
            if (value != null)
            {
                var stat = db.AerialStats.Find(id);

                if (stat != null)
                {
                    db.AerialStats.Add(stat);
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

        [Route("api/{id}/aerial")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stat = db.AerialStats.Find(id);

            if (stat != null)
            {
                db.AerialStats.Remove(stat);
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
