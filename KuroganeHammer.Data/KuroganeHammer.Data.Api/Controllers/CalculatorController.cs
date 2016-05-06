using System;
using System.Web.Http;
using KuroganeHammer.Data.Api.Models;
using KuroganeHammer.Data.Core.Calculations;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using System.Linq;
using KuroganeHammer.Data.Core;

namespace KuroganeHammer.Data.Api.Controllers
{
    [RoutePrefix("api")]
    public class CalculatorController : BaseApiController
    {
        private const string CalculatorRoutePrefix = "calculator";

        public CalculatorController()
        { }

        public CalculatorController(ApplicationDbContext context)
            : base(context)
        { }


        [Route(CalculatorRoutePrefix + "/rage")]
        [Authorize(Roles = Basic)]
        [HttpGet]
        public IHttpActionResult GetRage(RageProblemData data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calculator = new Calculator();
            var result = calculator.Rage(data);

            return Ok(result);
        }

        [Route(CalculatorRoutePrefix + "/moves/rage")]
        [Authorize(Roles = Basic)]
        [HttpGet]
        public IHttpActionResult GetMoveRage(CalculatorMoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calculator = new Calculator();
            var result = calculator.Rage(new RageProblemData { AttackerPercent = model.AttackerDamagePercent });

            return Ok(result);
        }

        [Route(CalculatorRoutePrefix + "/moves/vsknockback")]
        [Authorize(Roles = Basic)]
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

            return Ok(result);
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