using Kurogane.Data.RestApi.DTOs;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Model.Characters;

namespace Kurogane.Data.RestApi.Controllers
{
    public class MovementController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/movement")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> GetAllMovementStats()
        {
            return from movement in db.MovementStats.ToList()
                   select new MovementStatDTO
                   {
                       CharacterName = ((Characters)movement.OwnerId).ToString(),
                       Id = movement.Id,
                       Name = movement.Name,
                       OwnerId = movement.OwnerId,
                       Value = movement.Value,
                   };
        }

        [Route("api/movement/{id}")]
        [HttpGet]
        public MovementStatDTO Get(int id)
        {
            var movement = db.MovementStats.Find(id);
            return new MovementStatDTO
                                {
                                    CharacterName = ((Characters)movement.OwnerId).ToString(),
                                    Id = movement.Id,
                                    Name = movement.Name,
                                    OwnerId = movement.OwnerId,
                                    Value = movement.Value,
                                };
        }

        [Route("api/movement/{name}")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> GetAllMovementOfName(string name, [FromUri] string orderBy)
        {
            IEnumerable<MovementStatDTO> movementStats = default(IEnumerable<MovementStatDTO>);

            if (orderBy.Equals("desc"))
            {
                movementStats = from movement in db.MovementStats.ToList()
                                where movement.Name.Equals(name)
                                orderby movement.Value descending
                                select new MovementStatDTO
                                {
                                    CharacterName = ((Characters)movement.OwnerId).ToString(),
                                    Id = movement.Id,
                                    Name = movement.Name,
                                    OwnerId = movement.OwnerId,
                                    Value = movement.Value,
                                };
            }
            else
            {
                movementStats = from movement in db.MovementStats.ToList()
                                where movement.Name.Equals(name)
                                orderby movement.Value ascending
                                select new MovementStatDTO
                                {
                                    CharacterName = ((Characters)movement.OwnerId).ToString(),
                                    Id = movement.Id,
                                    Name = movement.Name,
                                    OwnerId = movement.OwnerId,
                                    Value = movement.Value,
                                };
            }
            return movementStats;

        }

        [Route("api/movement")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MovementStatDTO value)
        {
            if (value != null)
            {
                var stat = EntityBusinessConverter<MovementStatDTO>.ConvertTo<MovementStat>(value);

                if (stat != null)
                {
                    db.MovementStats.Attach(stat);
                    db.Entry(stat).State = EntityState.Added;
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

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]MovementStatDTO value)
        {
            if (value != null)
            {
                var stat = db.MovementStats.Find(id);

                if (stat != null)
                {
                    db.MovementStats.Add(stat);
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

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stat = db.MovementStats.Find(id);

            if (stat != null)
            {
                db.MovementStats.Remove(stat);
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
