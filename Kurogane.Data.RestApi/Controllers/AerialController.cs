using KuroganeHammer.Data.Core.Model.Stats;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Kurogane.Data.RestApi.DTOs;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Aerial")]
    public class AerialController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        /// <summary>
        /// Takes in the character ID and returns all of the found character's <see cref="AerialStatModel"/> data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<AerialStatModel> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<AerialStatModel> stats = default(IEnumerable<AerialStatModel>);

            if (character != null)
            {
                stats = from stat in db.AerialStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new AerialStatModel()
                        {
                            Angle = stat.Angle,
                            AutoCancel = stat.Autocancel,
                            BaseDamage = stat.BaseDamage,
                            BackKnockbackSetKnockback = stat.BaseKnockbackSetKnockback,
                            FirstActionableFrame = stat.FirstActionableFrame,
                            HitboxActive = stat.HitboxActive,
                            KnockbackGrowth = stat.KnockbackGrowth,
                            LandingLag = stat.LandingLag,
                            Name = stat.Name,
                            OwnerId = stat.OwnerId,
                            RawName = stat.RawName
                        };
            }

            return stats;
        }

        public IHttpActionResult Post([FromBody]AerialStat value)
        {
            if (value != null)
            {
                db.AerialStats.Add(value);
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
