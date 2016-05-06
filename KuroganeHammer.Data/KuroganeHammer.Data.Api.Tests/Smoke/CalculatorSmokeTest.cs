using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Api.DTOs;
using KuroganeHammer.Data.Api.Models;
using KuroganeHammer.Data.Core.Calculations;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Smoke
{
    [TestFixture]
    public class CalculatorSmokeTest : BaseSmokeTest
    {
        [Test]
        public async Task ShouldReturnExpectedVersusKnockback()
        {
            var requestModel = new CalculatorMoveModel
            {
                AttackerDamagePercent = 100,
                CrouchingModifier = CrouchingModifier.NotCrouching,
                ElectricModifier = ElectricModifier.NormalAttack,
                HitboxOption = HitboxOptions.First,
                Modifiers = Modifiers.Standing,
                MoveId = 2,
                ShieldAdvantageModifier = ShieldAdvantageModifier.Regular,
                StaleMoveNegationMultiplier = StaleMoveNegationMultipler.S1,
                TargetWeight = 80,
                VictimDamagePercent = 70
            };

            var response = await LoggedInBasicClient.PostAsJsonAsync(Baseuri + CalculatorRoute + "/moves/vsknockback", requestModel);

            Assert.That(response.IsSuccessStatusCode);

            var result = await response.Content.ReadAsAsync<CompletedCalculationResponseDto>();

            Assert.That(result.MoveName, Is.EqualTo("Dash Attack"));
            Assert.That(result.CalculatedValue, Is.EqualTo(469.38543478260868d));
        }
    }
}
