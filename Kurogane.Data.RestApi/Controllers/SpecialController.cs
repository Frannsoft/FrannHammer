using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Special")]
    public class SpecialController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        /// <summary>
        /// Get all of the <see cref="SpecialStatDTO"/> data for the character
        /// with the passed in ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<SpecialStatDTO> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<SpecialStatDTO> stats = default(IEnumerable<SpecialStatDTO>);

            if (character != null)
            {
                stats = from stat in db.SpecialStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new SpecialStatDTO()
                        {
                            Angle = stat.Angle,
                            BaseDamage = stat.BaseDamage,
                            BaseKnockbackSetKnockback = stat.BaseKnockbackSetKnockback,
                            FirstActionableFrame = stat.FirstActionableFrame,
                            HitboxActive = stat.HitboxActive,
                            KnockbackGrowth = stat.KnockbackGrowth,
                            Name = stat.Name,
                            OwnerId = stat.OwnerId,
                            RawName = stat.RawName
                        };
            }

            return stats;
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]SpecialStat value)
        {
            if (value != null)
            {
                var stat = db.SpecialStats.Find(value.Id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.SpecialStats.Add(value);
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
        public IHttpActionResult Patch(int id, [FromBody]SpecialStat value)
        {
            if (value != null)
            {
                var stat = db.SpecialStats.Find(id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.SpecialStats.Add(value);
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
            var stat = db.SpecialStats.Find(id);

            if (stat != null)
            {
                db.SpecialStats.Remove(stat);
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
