using System;
using System.Linq;
using System.Web.Http;
using FrannHammer.Api.DTOs;
using FrannHammer.Api.Models;
using FrannHammer.Core;
using FrannHammer.Core.Calculations;
using FrannHammer.Core.Models;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Controller used for calculating data off of character attributes and properties such as moves.
    /// </summary>
    [RoutePrefix("api")]
    public class CalculatorController : BaseApiController
    {
        private const string CalculatorRoutePrefix = "calculator";

        /// <summary>
        /// Creates a <see cref="CalculatorController"/> with the specified <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="context"></param>
        public CalculatorController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Calculate Rage based on passed in data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route(CalculatorRoutePrefix + "/rage")]
        [HttpPost]
        public IHttpActionResult PostRage(RageProblemData data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calculator = new Calculator();
            var result = calculator.Rage(data);

            return Ok(result);
        }

        //[Route(CalculatorRoutePrefix + "/moves/rage")]
        //[Authorize(Roles = Basic)]
        //[HttpGet]
        //public IHttpActionResult GetMoveRage(CalculatorMoveModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var calculator = new Calculator();
        //    var result = calculator.Rage(new RageProblemData { AttackerPercent = model.AttackerDamagePercent });

        //    return Ok(result);
        //}

        /// <summary>
        /// Calculate Vs mode knockback of a specific character's move.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route(CalculatorRoutePrefix + "/moves/vsknockback")]
        [HttpPost]
        public IHttpActionResult PostMoveVsKnockback(CalculatorMoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calculator = new Calculator();
            var result = calculator.VersusModeKnockback(new VersusModeKnockbackProblemData
            {
                BaseDamage = Convert.ToDouble(GetHitboxValue(Db.BaseDamage.Single(b => b.MoveId == model.MoveId), model.HitboxOption)),
                AttackerPercent = model.AttackerDamagePercent,
                BaseKnockbackSetKnockback =
                    Convert.ToInt32(GetHitboxValue(Db.BaseKnockbackSetKnockback.Single(b => b.MoveId == model.MoveId), model.HitboxOption)),
                KnockbackGrowth = Convert.ToInt32(GetHitboxValue(Db.KnockbackGrowth.Single(k => k.MoveId == model.MoveId), model.HitboxOption)),
                StaleMoveMultiplier = model.StaleMoveNegationMultiplier.GetModifierValue(),
                StanceModifier = model.Modifiers.GetModifierValue(),
                TargetWeight = model.TargetWeight,
                VictimPercent = model.VictimDamagePercent
            });

            var ownerId = Db.Moves.First(m => m.Id == model.MoveId).OwnerId;
            var calcResult = new CompletedCalculationResponseDto
            {
                CalculatedValueName = "VsModeKnockback",
                CharacterName = Db.Characters.First(c => c.Id == ownerId).Name,
                MoveName = Db.Moves.Find(model.MoveId).Name,
                CalculatedValue = result
            };

            return Ok(calcResult);
        }

        /// <summary>
        /// Calculate the Normal Shield stun of a specific character's move.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route(CalculatorRoutePrefix + "/moves/shieldstunnormal")]
        [HttpPost]
        public IHttpActionResult PostMoveShieldStunNormal(CalculatorMoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calculator = new Calculator();
            var move = Db.Moves.Find(model.MoveId);

            if (move == null)
            {
                return BadRequest($"Unable to find move with id: {model.MoveId}");
            }

            var damageAsDouble = Convert.ToDouble(move.BaseDamage);
            var result = calculator.ShieldStunNormal(damageAsDouble);

            var calcResult = new CompletedCalculationResponseDto
            {
                CalculatedValueName = "ShieldStunNormal",
                CalculatedValue = result,
                CharacterName = Db.Characters.Find(move.OwnerId).Name,
                MoveName = move.Name
            };

            return Ok(calcResult);
        }


        private string GetHitboxValue(BaseMoveHitboxMeta hitboxModel, HitboxOptions hitboxOption)
        {
            Guard.VerifyObjectNotNull(hitboxModel, nameof(hitboxModel));
            Guard.VerifyObjectNotNull(hitboxOption, nameof(hitboxOption));

            switch (hitboxOption)
            {
                case HitboxOptions.First:
                    {
                        return hitboxModel.Hitbox1;
                    }
                case HitboxOptions.Second:
                    {
                        return hitboxModel.Hitbox2;
                    }

                case HitboxOptions.Third:
                    {
                        return hitboxModel.Hitbox3;
                    }
                case HitboxOptions.Fourth:
                    {
                        return hitboxModel.Hitbox4;
                    }
                case HitboxOptions.Fifth:
                    {
                        return hitboxModel.Hitbox5;
                    }
                default:
                    {
                        throw new Exception($"{hitboxOption} not recognized.");
                    }
            }
        }

    }
}