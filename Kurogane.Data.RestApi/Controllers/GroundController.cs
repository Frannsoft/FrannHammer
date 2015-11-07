using Kurogane.Data.RestApi.DTOs;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;

namespace Kurogane.Data.RestApi.Controllers
{
    public class GroundController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/ground")]
        [HttpGet]
        public IEnumerable<GroundStatDTO> Get()
        {
            return from ground in db.GroundStats
                   select new GroundStatDTO()
                   {
                       Angle = ground.Angle,
                       BaseDamage = ground.BaseDamage,
                       BaseKnockbackSetKnockback = ground.BaseKnockbackSetKnockback,
                       FirstActionableFrame = ground.FirstActionableFrame,
                       HitboxActive = ground.HitBoxActive,
                       KnockbackGrowth = ground.KnockbackGrowth,
                       Name = ground.Name,
                       CharacterName = db.Characters.FirstOrDefault(s => s.OwnerId == ground.OwnerId).Name,
                       OwnerId = ground.OwnerId,
                       RawName = ground.RawName,
                       Id = ground.Id
                   };
        }

        [Route("api/characters/{id}/ground")]
        [HttpGet]
        public IEnumerable<GroundStatDTO> Get(int id)
        {
            return from ground in db.GroundStats
                   where ground.OwnerId == id
                   select new GroundStatDTO()
                   {
                       Angle = ground.Angle,
                       BaseDamage = ground.BaseDamage,
                       BaseKnockbackSetKnockback = ground.BaseKnockbackSetKnockback,
                       FirstActionableFrame = ground.FirstActionableFrame,
                       HitboxActive = ground.HitBoxActive,
                       KnockbackGrowth = ground.KnockbackGrowth,
                       Name = ground.Name,
                       CharacterName = db.Characters.FirstOrDefault(s => s.OwnerId == ground.OwnerId).Name,
                       OwnerId = ground.OwnerId,
                       RawName = ground.RawName,
                   };
        }


        [Route("api/{id}/ground")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]GroundStatDTO value)
        {
            if (value != null)
            {
                var stat = db.GroundStats.Find(value.Id);

                if (stat != null)
                {
                    db.GroundStats.Attach(stat);
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


        [Route("api/{id}/ground")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]GroundStatDTO value)
        {
            if (value != null)
            {
                var stat = db.GroundStats.Find(id);

                if (stat != null)
                {
                    db.GroundStats.Add(stat);
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

        [Route("api/{id}/ground")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stat = db.GroundStats.Find(id);

            if (stat != null)
            {
                db.GroundStats.Remove(stat);
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
