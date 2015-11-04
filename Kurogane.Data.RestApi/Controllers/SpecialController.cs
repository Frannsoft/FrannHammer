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
        /// Get all of the <see cref="SpecialStatModel"/> data for the character
        /// with the passed in ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<SpecialStatModel> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<SpecialStatModel> stats = default(IEnumerable<SpecialStatModel>);

            if (character != null)
            {
                stats = from stat in db.SpecialStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new SpecialStatModel()
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

        public IHttpActionResult Post([FromBody]SpecialStat value)
        {
            if (value != null)
            {
                db.SpecialStats.Add(value);
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
