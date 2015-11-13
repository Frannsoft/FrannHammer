using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    public class MoveController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/moves")]
        [HttpGet]
        public IEnumerable<MoveDTO> GetMoves()
        {
            return from move in db.Moves.ToList()
                   select EntityBusinessConverter<MoveStat>.ConvertTo<MoveDTO>(move);
        }

        [Route("api/movesoftype/{type}")]
        [HttpGet]
        public IEnumerable<MoveDTO> GetMovesOfType(MoveType type)
        {
            return from move in db.Moves.ToList()
                   where move.Type == type
                   select EntityBusinessConverter<MoveStat>.ConvertTo<MoveDTO>(move);
        }

        [Route("api/moves/{name}")]
        [HttpGet]
        public IEnumerable<MoveDTO> GetMovesOfName(string name, [FromUri] string orderBy)
        {
            IEnumerable<MoveDTO> moveStats = default(IEnumerable<MoveDTO>);

            if (orderBy.Equals("desc"))
            {
                moveStats =  from move in db.Moves.ToList()
                       where move.Name == name
                       orderby move.FirstActionableFrame descending
                       select new MoveDTO
                       {
                           Angle = move.Angle,
                           AutoCancel = move.AutoCancel,
                           BaseDamage = move.BaseDamage,
                           BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback,
                           FirstActionableFrame = move.FirstActionableFrame,
                           HitboxActive = move.HitboxActive,
                           Id = move.Id,
                           KnockbackGrowth = move.KnockbackGrowth,
                           LandingLag = move.LandingLag,
                           Name = move.Name,
                           OwnerId = move.OwnerId,
                           Type = move.Type,
                           CharacterName = ((Characters)move.OwnerId).ToString()
                       };
            }
            else
            {
                moveStats = from move in db.Moves.ToList()
                       where move.Name == name
                            orderby move.FirstActionableFrame ascending
                       select new MoveDTO
                       {
                           Angle = move.Angle,
                           AutoCancel = move.AutoCancel,
                           BaseDamage = move.BaseDamage,
                           BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback,
                           FirstActionableFrame = move.FirstActionableFrame,
                           HitboxActive = move.HitboxActive,
                           Id = move.Id,
                           KnockbackGrowth = move.KnockbackGrowth,
                           LandingLag = move.LandingLag,
                           Name = move.Name,
                           OwnerId = move.OwnerId,
                           Type = move.Type,
                           CharacterName = ((Characters)move.OwnerId).ToString()
                       };
            }
            return moveStats;
        }

        [Route("api/moves/{id}")]
        [HttpGet]
        public MoveDTO Get(int id)
        {
            var move = db.Moves.Find(id);

            if (move != null)
            {
                return EntityBusinessConverter<MoveStat>.ConvertTo<MoveDTO>(move);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]MoveDTO value)
        {
            if (value != null)
            {
                MoveStat stat = EntityBusinessConverter<MoveDTO>.ConvertTo<MoveStat>(value);
                if (stat != null)
                {
                    db.Moves.Attach(stat);
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
        public IHttpActionResult Put(int id, [FromBody]MoveDTO value)
        {
            if (value != null)
            {
                var stat = db.Moves.Find(id);

                if (stat != null)
                {
                    db.Moves.Add(stat);
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
            var stat = db.Moves.Find(id);

            if (stat != null)
            {
                db.Moves.Remove(stat);
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
