using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Ground")]
    public class GroundController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        /// <summary>
        /// Get all of the <see cref="GroundStatDTO"/> data for the character
        /// with the passed in ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<GroundStatDTO> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<GroundStatDTO> stats = default(IEnumerable<GroundStatDTO>);

            if (character != null)
            {
                stats = from stat in db.GroundStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new GroundStatDTO()
                        {
                            Angle = stat.Angle,
                            BaseDamage = stat.BaseDamage,
                            BaseKnockbackSetKnockback = stat.BaseKnockbackSetKnockback,
                            FirstActionableFrame = stat.FirstActionableFrame,
                            HitboxActive = stat.HitBoxActive,
                            KnockbackGrowth = stat.KnockbackGrowth,
                            Name = stat.Name,
                            OwnerId = stat.OwnerId,
                            RawName = stat.RawName,
                        };
            }

            return stats;
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]GroundStat value)
        {
            if (value != null)
            {
                var stat = db.GroundStats.Find(value.Id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.GroundStats.Add(value);
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
        public IHttpActionResult Patch(int id, [FromBody]GroundStat value)
        {
            if (value != null)
            {
                var stat = db.GroundStats.Find(value.Id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.GroundStats.Add(value);
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
            var stat = db.GroundStats.Find(id);

            if (stat != null)
            {
                db.GroundStats.Remove(stat);
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
