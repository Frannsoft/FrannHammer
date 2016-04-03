using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using KuroganeHammer.Data.Api.Models;

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
        public IQueryable<SmashAttributeType> GetSmashAttributeTypes()
        {
            return db.SmashAttributeTypes;
        }

        // GET: api/SmashAttributeTypes/5
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(SmashAttributeType))]
        public IHttpActionResult GetSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            return Ok(smashAttributeType);
        }

        // PUT: api/SmashAttributeTypes/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
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
        public IHttpActionResult PostSmashAttributeType(SmashAttributeType smashAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            smashAttributeType.LastModified = DateTime.Now;
            db.SmashAttributeTypes.Add(smashAttributeType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = smashAttributeType.Id }, smashAttributeType);
        }

        // DELETE: api/SmashAttributeTypes/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(SmashAttributeType))]
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