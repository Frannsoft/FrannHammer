﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.DTOs;
using FrannHammer.Api.Models;
using FrannHammer.Core.Models;

namespace FrannHammer.Api.Controllers
{
    [RoutePrefix("api")]
    public class MovesController : BaseApiController
    {
        public MovesController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all moves.  Not sure if this is sticking around.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MoveDto>))]
        [Route("moves")]
        public IQueryable<MoveDto> GetMoves()
        {
            return (from move in Db.Moves.ToList()
                    select new MoveDto(move,
                        Db.Characters.Find(move.OwnerId))// .First(c => c.Id == move.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Get all moves that have a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MoveDto>))]
        [Route("moves/byname")]
        public IQueryable<MoveDto> GetMovesByName([FromUri] string name)
        {
            return (from move in Db.Moves.Where(m => m.Name.Equals(name)).ToList()
                    select new MoveDto(move,
                        Db.Characters.First(c => c.Id == move.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Get a specific <see cref="MoveDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [Route("moves/{id}")]
        public IHttpActionResult GetMove(int id)
        {
            Move move = Db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            var dto = new MoveDto(move,
                Db.Characters.First(c => c.Id == move.OwnerId));
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(IHttpActionResult))]
        [Route("moves/{id}")]
        public IHttpActionResult PutMove(int id, Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != move.Id)
            {
                return BadRequest();
            }

            move.LastModified = DateTime.Now;
            Db.Entry(move).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoveExists(id))
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

        /// <summary>
        /// Create a new <see cref="Move"/>.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Move))]
        [Route("moves")]
        public IHttpActionResult PostMove(Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            move.LastModified = DateTime.Now;
            Db.Moves.Add(move);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "Moves", id = move.Id }, move);
        }

        /// <summary>
        /// Delete a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Move))]
        [Route("moves/{id}")]
        public IHttpActionResult DeleteMove(int id)
        {
            Move move = Db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            Db.Moves.Remove(move);
            Db.SaveChanges();

            return Ok(move);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoveExists(int id)
        {
            return Db.Moves.Count(e => e.Id == id) > 0;
        }
    }
}