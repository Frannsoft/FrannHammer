using KuroganeHammer.Data.Core.Calculations;
using NUnit.Framework;

namespace KuroganeHammer.Data.Core.Tests
{
    [TestFixture]
    public class CalculateTest
    {
        [Test]
        public void ShouldGetExpectedTrainingModeKnockback()
        {
            var calculate = new Calculator();
            var result = calculate.TrainingModeKnockback(100, 20, 80, 10, 31, 1);

            Assert.That(result, Is.EqualTo(53.333333333333336d));
        }

        [Test]
        [TestCase(Modifiers.ChargingSmash, 1.2)]
        [TestCase(Modifiers.CrouchCancelling, 0.85)]
        [TestCase(Modifiers.GroundedMeteor, 0.8)]
        [TestCase(Modifiers.Standing, 1)]
        public void ShouldGetValueAttributeFromModifier_ChargingSmash(Modifiers modifier, double expectedValue)
        {
            Assert.That(modifier.GetModifierValue(), Is.EqualTo(expectedValue));
        }
    }
}
