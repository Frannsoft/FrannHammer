using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api/Movement")]
    public class MovementController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        /// <summary>
        /// Takes in the character ID and returns all of the found 
        /// character's <see cref="MovementStatDTO"/> data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MovementStatDTO> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<MovementStatDTO> stats = default(IEnumerable<MovementStatDTO>);

            if (character != null)
            {
                stats = from stat in db.MovementStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new MovementStatDTO()
                        {
                            Name = stat.Name,
                            OwnerId = stat.OwnerId,
                            Rank = stat.Rank,
                            RawName = stat.RawName,
                            Value = stat.Value
                        };
            }

            return stats;
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]MovementStat value)
        {
            if (value != null)
            {
                var stat = db.MovementStats.Find(value.Id);

                if(stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.MovementStats.Add(value);
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
        public IHttpActionResult Patch(int id, [FromBody]MovementStat value)
        {
            if (value != null)
            {
                var stat = db.MovementStats.Find(id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.MovementStats.Add(value);
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
            var stat = db.MovementStats.Find(id);

            if (stat != null)
            {
                db.MovementStats.Remove(stat);
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
