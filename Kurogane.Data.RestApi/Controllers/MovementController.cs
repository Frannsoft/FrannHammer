﻿using Kurogane.Data.RestApi.DTOs;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using KuroganeHammer.Data.Core.Model.Stats;
using KuroganeHammer.Data.Core;

namespace Kurogane.Data.RestApi.Controllers
{
    public class MovementController : ApiController
    {
        private Sm4shContext db = new Sm4shContext();

        [Route("api/movement")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> Get()
        {
            return from movement in db.MovementStats.ToList()
                   select EntityBusinessConverter<MovementStat>.ConvertTo<MovementStatDTO>(movement);
        }

        [Route("api/characters/{id}/movement")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> Get(int id)
        {
            return from movement in db.MovementStats.ToList()
                   where movement.OwnerId == id
                   select EntityBusinessConverter<MovementStat>.ConvertTo<MovementStatDTO>(movement);
        }

        [Route("api/{id}/movement")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MovementStatDTO value)
        {
            if (value != null)
            {
                var stat = db.MovementStats.Find(value.Id);

                if (stat != null)
                {
                    db.MovementStats.Attach(stat);
                    db.Entry(stat).State = EntityState.Modified;
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

        [Route("api/{id}/movement")]
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

        [Route("api/{id}/movement")]
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
