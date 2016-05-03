using KuroganeHammer.Data.Core.Calculations;
using NUnit.Framework;

namespace KuroganeHammer.Data.Core.Tests
{
    [TestFixture]
    public class CalculateTest
    {
        private Calculator _calculator;
        private TrainingModeKnockbackProblemData _trainingModeKnockbackData;
        private VersusModeKnockbackProblemData _versusModeKnockbackData;

        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
            _trainingModeKnockbackData = new TrainingModeKnockbackProblemData
            {
                VictimPercent = 100,
                BaseDamage = 20,
                TargetWeight = 80,
                KnockbackGrowth = 10,
                BaseKnockbackSetKnockback = 31,
                StanceModifier = 1
            };

            _versusModeKnockbackData = new VersusModeKnockbackProblemData
            {
                VictimPercent = 100,
                BaseDamage = 20,
                TargetWeight = 80,
                KnockbackGrowth = 10,
                BaseKnockbackSetKnockback = 31,
                StanceModifier = 1,
                StaleMoveMultiplier = 0
            };
        }

        [Test]
        public void ShouldGetExpectedTrainingModeKnockback()
        {
            var result = _calculator.TrainingModeKnockback(_trainingModeKnockbackData);
            Assert.That(result, Is.EqualTo(53.333333333333336d));
        }

        [Test]
        public void ShouldGetExpectedVersusModeKnockback()
        {
            var result = _calculator.VersusModeKnockback(_versusModeKnockbackData);
            Assert.That(result, Is.EqualTo(49.080386473429961d));
        }

        [Test]
        public void ShouldGetExpectedTrainingModeHitstun()
        {
            var trainingModeKnockback = _calculator.TrainingModeKnockback(_trainingModeKnockbackData);
            var result = _calculator.Hitstun(trainingModeKnockback);
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void ShouldGetExpectedVersusModeHitstun()
        {
            var versusModeKnockback = _calculator.VersusModeKnockback(_versusModeKnockbackData);
            var result = _calculator.Hitstun(versusModeKnockback);
            Assert.That(result, Is.EqualTo(18));
        }

        [Test]
        [TestCase(8.0, 6)]
        [TestCase(25.5, 16)]
        public void ShouldGetExpectedShieldstun_Normal(double damage, int expectedShieldstun)
        {
            var result = _calculator.ShieldStunNormal(damage);
            Assert.That(result, Is.EqualTo(expectedShieldstun));
        }

        [Test]
        public void ShouldGetExpectedRage()
        {
            var result = _calculator.Rage(100);
            Assert.That(result, Is.EqualTo(1.0847826086956522d));
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
