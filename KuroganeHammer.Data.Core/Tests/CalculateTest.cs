using System;
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
        [TestCase(8.0, 5)]
        [TestCase(25.5, 11)]
        public void ShouldGetExpectedShieldstun_Powershield(double damage, int expectedShieldstun)
        {
            var result = _calculator.ShieldStunPowerShield(damage);
            Assert.That(result, Is.EqualTo(expectedShieldstun));
        }

        [Test]
        [TestCase(8.0, 4)]
        [TestCase(25.5, 9)]
        public void ShouldGetExpectedShieldstun_Projectile(double damage, int expectedShieldstun)
        {
            var result = _calculator.ShieldStunProjectile(damage);
            Assert.That(result, Is.EqualTo(expectedShieldstun));
        }

        [Test]
        [TestCase(8.0, 3)]
        [TestCase(25.5, 6)]
        public void ShouldGetExpectedShieldstun_Powershield_Projectile(double damage, int expectedShieldstun)
        {
            var result = _calculator.ShieldStunPowerShieldProjectile(damage);
            Assert.That(result, Is.EqualTo(expectedShieldstun));
        }

        [Test]
        public void ShouldGetExpectedHitlag_NotCrouching_NormalAttack()
        {
            var hitlagData = new HitlagProblemData
            {
                Damage = 20,
                CrouchingModifier = CrouchingModifier.NotCrouching,
                ElectricModifier = ElectricModifier.NormalAttack,
                HitlagMultiplier = 1
            };

            var result = _calculator.Hitlag(hitlagData);
            Assert.That(result, Is.EqualTo(11));
        }

        [Test]
        public void ShouldGetExpectedHitlag_NotCrouching_ElectricAttack()
        {
            var hitlagData = new HitlagProblemData
            {
                Damage = 20,
                CrouchingModifier = CrouchingModifier.NotCrouching,
                ElectricModifier = ElectricModifier.ElectricAttack,
                HitlagMultiplier = 1
            };

            var result = _calculator.Hitlag(hitlagData);
            Assert.That(result, Is.EqualTo(18));
        }

        [Test]
        [TestCase(20, CrouchingModifier.NotCrouching, ElectricModifier.NormalAttack, 1, 11)]
        [TestCase(20, CrouchingModifier.NotCrouching, ElectricModifier.NormalAttack, 2, 24)]
        [TestCase(20, CrouchingModifier.Crouching, ElectricModifier.ElectricAttack, 1, 11)]
        [TestCase(20, CrouchingModifier.Crouching, ElectricModifier.ElectricAttack, 2, 24)]
        public void ShouldGetExpectedHitlag(double damage, CrouchingModifier crouchModifier,
            ElectricModifier elecModifier, double hitlagMultiplier, int expectedResult)
        {
            var hitlagData = new HitlagProblemData
            {
                Damage = damage,
                CrouchingModifier = crouchModifier,
                ElectricModifier = elecModifier,
                HitlagMultiplier = hitlagMultiplier
            };

            var result = _calculator.Hitlag(hitlagData);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        public void ShouldReturn30HitlagWhenHitlagMoreThan30()
        {
            var hitlagData = new HitlagProblemData
            {
                Damage = 50,
                CrouchingModifier = CrouchingModifier.NotCrouching,
                ElectricModifier = ElectricModifier.NormalAttack,
                HitlagMultiplier = 2
            };

            var result = _calculator.Hitlag(hitlagData);
            Assert.That(result, Is.EqualTo(30));
        }

        [Test]
        public void ShouldGetExpectedShieldAdvantage_Normal()
        {
            var data = new ShieldAdvantageProblemData
            {
                FirstActionableFrame = 22,
                HitFrame = 20,
                Hitlag = 11,
                ShieldStun = 13,
                ShieldAdvantageModifier = ShieldAdvantageModifier.Regular,
                HitlagData = new HitlagProblemData
                {
                    CrouchingModifier = CrouchingModifier.NotCrouching,
                    Damage = 20,
                    ElectricModifier = ElectricModifier.NormalAttack,
                    HitlagMultiplier = 1
                }
            };

            var result = _calculator.ShieldAdvantage(data);
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void ShouldGetExpectedShieldAdvantage_Normal_Projectile_NotHitlag()
        {
            var data = new ShieldAdvantageProblemData
            {
                FirstActionableFrame = 22,
                HitFrame = 20,
                Hitlag = 11,
                ShieldStun = 13,
                ShieldAdvantageModifier = ShieldAdvantageModifier.Projectile_NotHitlag,
                HitlagData = new HitlagProblemData
                {
                    CrouchingModifier = CrouchingModifier.NotCrouching,
                    Damage = 20,
                    ElectricModifier = ElectricModifier.NormalAttack,
                    HitlagMultiplier = 1
                }
            };

            var result = _calculator.ShieldAdvantage(data);
            Assert.That(result, Is.EqualTo(24), "Fail expected.  Bug in official calc up where it doesn't Floor when proj shield adv mod.");
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

        [Test]
        public void ShouldGetExpectedLedgeIntangibility()
        {
            var data = new LedgeIntangiblityProblemData
            {
                AirborneFrames = 50,
                CharacterPercentage = 70
            };

            var result = _calculator.LedgeIntangibility(data);
            Assert.That(result, Is.EqualTo(48));
        }

        [Test]
        public void ShouldThrowExceptionForAirFramesOver300_LedgeIntangibility()
        {
            var data = new LedgeIntangiblityProblemData
            {
                AirborneFrames = 301,
                CharacterPercentage = 80
            };

            Assert.Throws<Exception>(() => _calculator.LedgeIntangibility(data));
        }

        [Test]
        public void ShouldThrowExceptionForCharacterPercentageOver120()
        {
            var data = new LedgeIntangiblityProblemData
            {
                AirborneFrames = 30,
                CharacterPercentage = 121
            };

            Assert.Throws<Exception>(() => _calculator.LedgeIntangibility(data));
        }

        [Test]
        [Ignore("Need to contact spacejam about this formula")]
        public void ShouldGetExpectedReboundDuration()
        {
            var data = new ReboundDurationProblemData
            {
                Damage = 50
            };

            var result = _calculator.ReboundDuration(data);

            //Assert.That(result, Is.EqualTo())
        }

        [Test]
        [TestCase(ControllerInput.LStick, 116)]
        [TestCase(ControllerInput.Button, 109)]
        public void ShouldGetExpectedGrabDuration(ControllerInput input, int expectedFrames)
        {
            var data = new GrabDurationProblemData
            {
                Input = input,
                TargetPercent = 20
            };

            var result = _calculator.GrabDuration(data);
            Assert.That(result, Is.EqualTo(expectedFrames));
        }

        [Test]
        [TestCase(PikminGrabControllerInput.LStick, 333)]
        [TestCase(PikminGrabControllerInput.Button, 327)]
        public void ShouldGetExpectedPikminGrabDuration(PikminGrabControllerInput input, int expectedFrames)
        {
            var data = new PikminGrabDurationProblemData
            {
                Input = input,
                TargetPercent = 20
            };

            var result = _calculator.PikminGrabDuration(data);
            Assert.That(result, Is.EqualTo(expectedFrames));
        }

        [Test]
        [TestCase(SmashChargeModifier.Default, 13)]
        [TestCase(SmashChargeModifier.MegamanFSmash, 23)]
        public void ShouldGetExpectedSmashCharge(SmashChargeModifier chargeModifier, int expected)
        {
            var data = new SmashChargeProblemData
            {
                Damage = 40,
                HeldFrames = 50,
                SmashChargeModifier = chargeModifier
            };

            var result = _calculator.SmashCharge(data);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
