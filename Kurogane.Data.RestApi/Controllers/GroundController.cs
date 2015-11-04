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

        /// <summary>
        /// test
        /// </summary>
        /// <param name="value">zzzz</param>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Post([FromBody]GroundStat value)
        {
            if (value != null)
            {
                db.GroundStats.Add(value);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return InternalServerError();
            }

        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
