using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KuroganeHammer.Data.Api.DTOs;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using KuroganeHammer.Data.Api.Models;
using Newtonsoft.Json;

namespace KuroganeHammer.Data.Api.Controllers
{
    [RoutePrefix("api")]
    public class SmashAttributeTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public SmashAttributeTypesController()
        { }

        public SmashAttributeTypesController(ApplicationDbContext context)
        {
            db = context;
        }


        // GET: api/SmashAttributeTypes
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(IQueryable<SmashAttributeType>))]
        [Route("smashattributetypes")]
        public IQueryable<SmashAttributeType> GetSmashAttributeTypes()
        {
            return db.SmashAttributeTypes;
        }

        // GET: api/SmashAttributeTypes/5
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(SmashAttributeType))]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult GetSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            return Ok(smashAttributeType);
        }

        [Authorize(Roles = Basic)]
        [Route("smashattributetypes/{id}/characterattributes")]
        public IHttpActionResult GetAllCharacterAttributeOfSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            //create a 'row' from each pulled back characterattribute since a characterattribute only represents
            //a single cell of a row in the existing KH site.
            var characterAttributeRows =
                db.CharacterAttributes.Where(c => c.SmashAttributeType.Id == smashAttributeType.Id)
                    .ToList() //execute query and bring into memory so I can continue to query against the data below
                    .GroupBy(a => a.OwnerId)
                    .Select(g => new CharacterAttributeRowDto(g.First().Rank, smashAttributeType.Id,
                        g.Key, g.Select(at => new CharacterAttributeKeyValuePair()
                        {
                            KeyName = at.Name,
                            ValueCharacterAttributeDto = new CharacterAttributeDto
                            {
                                Id = at.Id,
                                Name = at.Name,
                                OwnerId = at.OwnerId,
                                Rank = at.Rank,
                                SmashAttributeTypeDto = new SmashAttributeTypeDto
                                {
                                    Id = at.SmashAttributeType.Id,
                                    Name = at.Name
                                },
                                SmashAttributeTypeId = at.SmashAttributeTypeId,
                                Value = at.Value
                            }
                        }).ToList(),
                        db.Characters.Find(g.Key).Name, db.Characters.Find(g.Key).ThumbnailUrl))
                        .ToList();

            return Ok(characterAttributeRows);
        }

        // PUT: api/SmashAttributeTypes/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult PutSmashAttributeType(int id, SmashAttributeType smashAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != smashAttributeType.Id)
            {
                return BadRequest();
            }

            smashAttributeType.LastModified = DateTime.Now;
            db.Entry(smashAttributeType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmashAttributeTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SmashAttributeTypes
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(SmashAttributeType))]
        [Route("smashattributetypes")]
        public IHttpActionResult PostSmashAttributeType(SmashAttributeType smashAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            smashAttributeType.LastModified = DateTime.Now;
            db.SmashAttributeTypes.Add(smashAttributeType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller="SmashAttributeTypes", id = smashAttributeType.Id }, smashAttributeType);
        }

        // DELETE: api/SmashAttributeTypes/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(SmashAttributeType))]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult DeleteSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            db.SmashAttributeTypes.Remove(smashAttributeType);
            db.SaveChanges();

            return Ok(smashAttributeType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SmashAttributeTypeExists(int id)
        {
            return db.SmashAttributeTypes.Count(e => e.Id == id) > 0;
        }
    }
}