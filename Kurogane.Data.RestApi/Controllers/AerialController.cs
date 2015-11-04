﻿using KuroganeHammer.Data.Core.Model.Stats;
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
        /// Takes in the character ID and returns all of the found character's <see cref="AerialStatDTO"/> data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<AerialStatDTO> Get(int id)
        {
            var character = (from c in db.Characters.ToList()
                             where c.Id == id
                             select c).FirstOrDefault();

            IEnumerable<AerialStatDTO> stats = default(IEnumerable<AerialStatDTO>);

            if (character != null)
            {
                stats = from stat in db.AerialStats.ToList()
                        where stat.OwnerId == character.OwnerId
                        select new AerialStatDTO()
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

        [Route("")]
        public IHttpActionResult Post([FromBody]AerialStat value)
        {
            if (value != null)
            {
                var stat = db.AerialStats.Find(value.Id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.AerialStats.Add(value);
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
        public IHttpActionResult Patch(int id, [FromBody]AerialStat value)
        {
            if (value != null)
            {
                var stat = db.AerialStats.Find(id);

                if (stat != null)
                {
                    stat = value;
                }
                else
                {
                    db.AerialStats.Add(value);
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
            var stat = db.AerialStats.Find(id);

            if (stat != null)
            {
                db.AerialStats.Remove(stat);
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
