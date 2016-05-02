using System.Web.Http;
using KuroganeHammer.Data.Api.Models;
using KuroganeHammer.Data.Core.Calculations;


namespace KuroganeHammer.Data.Api.Controllers
{
    [RoutePrefix("api")]
    public class CalculatorController : BaseApiController
    {
        public CalculatorController()
        { }

        public CalculatorController(ApplicationDbContext context)
            : base(context)
        { }


        [Route("calculator")]
        public IHttpActionResult PostCalculatorRequest(CalculatorRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calculator = new Calculator();
            var calcResponseModel = new CalculatorResponseModel
            {
                TrainingModeKnockback =
                    calculator.TrainingModeKnockback(model.VictimPercent, model.BaseDamage, model.TargetWeight,
                        model.KnockbackGrowth, model.BaseKnockbackSetKnockback,
                        model.Modifiers.GetModifierValue())
            };

            return Ok(calcResponseModel);
        }
    }
}