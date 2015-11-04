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
        /// character's <see cref="MovementStatModel"/> data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MovementStatModel> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<MovementStatModel> stats = default(IEnumerable<MovementStatModel>);

            if (character != null)
            {
                stats = from stat in db.MovementStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new MovementStatModel()
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

        public IHttpActionResult Post([FromBody]MovementStat value)
        {
            {
                if (value != null)
                {
                    db.MovementStats.Add(value);
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }

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
