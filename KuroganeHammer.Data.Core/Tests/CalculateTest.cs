using KuroganeHammer.Data.Core.Calculations;
using NUnit.Framework;

namespace KuroganeHammer.Data.Core.Tests
{
    [TestFixture]
    public class CalculateTest
    {
        private Calculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void ShouldGetExpectedTrainingModeKnockback()
        {
            var result = _calculator.TrainingModeKnockback(100, 20, 80, 10, 31, 1);

            Assert.That(result, Is.EqualTo(53.333333333333336d));
        }

        [Test]
        public void ShouldGetExpectedVersusModeKnockback()
        {
            var result = _calculator.VersusModeKnockback(100, 20, 0, 80, 10, 31, 1);

            Assert.That(result, Is.EqualTo(49.080386473429961d));
        }

        [Test]
        [TestCase(Modifiers.ChargingSmash, 1.2)]
        [TestCase(Modifiers.CrouchCancelling, 0.85)]
        [TestCase(Modifiers.GroundedMeteor, 0.8)]
        [TestCase(Modifiers.Standing, 1)]
        public void ShouldGetValueAttributeFromModifier(Modifiers modifier, double expectedValue)
        {
            Assert.That(modifier.GetModifierValue(), Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase(ElectricModifier.NormalAttack, 1)]
        [TestCase(ElectricModifier.ElectricAttack, 1.5)]
        public void ShouldGetValueAttributeFromElectricModifier(ElectricModifier electricModifier, double expectedValue)
        {
            Assert.That(electricModifier.GetModifierValue(), Is.EqualTo(expectedValue));
        }
    }
}
