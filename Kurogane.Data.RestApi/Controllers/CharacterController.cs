using Kurogane.Data.RestApi.DTOs;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Controllers
{
    public class CharacterController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/characters")]
        [HttpGet]
        public IEnumerable<CharacterDTO> GetRoster()
        {
            return from chars in db.Characters.ToList()
                   orderby chars.Name ascending
                   select EntityBusinessConverter<CharacterStat>.ConvertTo<CharacterDTO>(chars);
        }

        [Route("api/characters/{id}")]
        [HttpGet]
        public CharacterDTO GetCharacter(int id)
        {
            //TODO - THIS NEEDS TO BE SHIFTED TO USE OWNERID AS A KEY NOT REGULAR ID SINCE
            //AZURE DOES NOT RESET IDS? (OR DROP TABLE THEN REINSERT)
            if (id > 0)
            {
                var characters = db.Characters.Find(id);

                if (characters != null)
                {
                    return EntityBusinessConverter<CharacterStat>.ConvertTo<CharacterDTO>(characters);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [Route("api/characters/{id}/movement")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> GetMovementForRoster(int id)
        {
            return from movement in db.MovementStats.ToList()
                   where movement.OwnerId == id
                   select EntityBusinessConverter<MovementStat>.ConvertTo<MovementStatDTO>(movement);
        }

        [Route("api/characters/{id}/moves")]
        [HttpGet]
        public IEnumerable<MoveDTO> GetMoves(int id)
        {
            return from move in db.Moves.ToList()
                   where move.OwnerId == id
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
                            CharacterName = ((Characters)move.OwnerId).ToString(),
                            CharacterThumbnailUrl = (from ch in db.Characters.ToList()
                                                     where ch.OwnerId == move.OwnerId
                                                     select ch.ThumbnailUrl).First()
                        };
        }

        [Route("api/characters/{id}/moves/{type}")]
        [HttpGet]
        public IEnumerable<MoveDTO> GetMoveForCharacterOfType(int id, MoveType type)
        {
            return from movement in db.Moves.ToList()
                   where movement.OwnerId == id &&
                   movement.Type == type
                   select EntityBusinessConverter<MoveStat>.ConvertTo<MoveDTO>(movement);
        }

        [Route("api/characters")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CharacterDTO value)
        {
            if (value != null)
            {
                CharacterStat stat = EntityBusinessConverter<CharacterDTO>.ConvertTo<CharacterStat>(value);
                if (stat != null)
                {
                    db.Characters.Add(stat);
                    db.Entry(stat).State = EntityState.Added;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Unable to create backend object.");
                }
            }
            else
            {
                return BadRequest("Parameter null.");
            }
        }

        [Route("api/characters/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CharacterDTO value)
        {
            if (value != null)
            {
                var stat = db.Characters.Find(id);

                stat.Description = value.Description;
                stat.MainImageUrl = value.MainImageUrl;
                stat.ThumbnailUrl = value.ThumbnailUrl;
                stat.Name = value.Name;
                stat.OwnerId = value.OwnerId;
                stat.Style = value.Style;
                if (stat != null)
                {
                    db.Characters.Attach(stat);
                    db.Entry(stat).State = EntityState.Modified;
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

        [Route("api/characters/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stat = db.Characters.Find(id);

            if (stat != null)
            {
                db.Characters.Remove(stat);
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
