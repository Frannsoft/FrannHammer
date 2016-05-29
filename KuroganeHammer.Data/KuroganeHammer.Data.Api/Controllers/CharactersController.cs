﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KuroganeHammer.Data.Api.Models;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using System;
using KuroganeHammer.Data.Api.DTOs;

namespace KuroganeHammer.Data.Api.Controllers
{
    /// <summary>
    /// Handles server operations dealing with <see cref="Character"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class CharactersController : BaseApiController
    {

        /// <summary>
        /// Create a new <see cref="CharactersController"/> to interact with the server.
        /// </summary>
        public CharactersController()
        { }

        /// <summary>
        /// Create a new <see cref="CharactersController"/> to interact with the server using 
        /// a specific <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public CharactersController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all of the <see cref="Character"/> details.
        /// </summary>
        /// <returns></returns>
        [Route("characters")]
        public IQueryable<Character> GetCharacters()
        {
            return Db.Characters;
        }

        /// <summary>
        /// Get a specific <see cref="Character"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Character))]
        [Route("characters/{id}")]
        public IHttpActionResult GetCharacter(int id)
        {
            Character character = Db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        /// <summary>
        /// Get all the <see cref="Movement"/> data for a specific <see cref="Character"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Characters/{id}/movements")]
        [HttpGet]
        public IQueryable<MovementDto> GetMovementsForCharacter(int id)
        {
            return (from movement in Db.Movements.Where(m => m.OwnerId == id).ToList()
                    select new MovementDto(movement,
                    Db.Characters.First(c => c.Id == movement.OwnerId))
               ).AsQueryable();
        }

        /// <summary>
        /// Get all the <see cref="Move"/> data for a specific <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Characters/{id}/moves")]
        public IQueryable<MoveDto> GetMovesForCharacter(int id)
        {
            return (from move in Db.Moves.Where(m => m.OwnerId == id).ToList()
                    select new MoveDto(move,
                        Db.Characters.First(c => c.Id == move.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Update a <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        [Route("characters/{id}")]
        public IHttpActionResult PutCharacter(int id, Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.Id)
            {
                return BadRequest();
            }

            character.LastModified = DateTime.Now;
            Db.Entry(character).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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
        /// Create a new <see cref="Character"/>.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(Character))]
        [Route("characters")]
        public IHttpActionResult PostCharacter(Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.Characters.Add(character);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "Characters", id = character.Id }, character);
        }

        /// <summary>
        /// Delete a <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(Character))]
        [Route("characters/{id}")]
        public IHttpActionResult DeleteCharacter(int id)
        {
            Character character = Db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            Db.Characters.Remove(character);
            Db.SaveChanges();

            return Ok(character);
        }

        private bool CharacterExists(int id)
        {
            return Db.Characters.Count(e => e.Id == id) > 0;
        }
    }
}