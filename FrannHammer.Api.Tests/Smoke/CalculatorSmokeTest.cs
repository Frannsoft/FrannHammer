﻿using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Api.DTOs;
using FrannHammer.Core.Calculations;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Smoke
{
    [TestFixture]
    public class CalculatorSmokeTest : BaseSmokeTest
    {
        [Test]
        [Ignore("Still working to setup Owin self hosting")]
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

            var response =
                await
                    LoggedInBasicClient.PostAsJsonAsync(Baseuri + CalculatorRoute + "/moves/vsknockback", requestModel);

            Assert.That(response.IsSuccessStatusCode);

            var result = await response.Content.ReadAsAsync<CompletedCalculationResponseDto>();

            Assert.That(result.CharacterName, Is.EqualTo("Pikachu"));
            Assert.That(result.CalculatedValueName, Is.EqualTo("VsModeKnockback"));
            Assert.That(result.MoveName, Is.EqualTo("Dash Attack"));
            Assert.That(result.CalculatedValue, Is.EqualTo(469.38543478260868d));
        }

        [Test]
        [Explicit("Writes to csv file")]
        public async Task WriteToCSVTest_VsModeKnockback()
        {
            var responses = new List<CompletedCalculationResponseDto>();

            var moves = await LoggedInBasicClient.GetAsync(Baseuri + MovesRoute);

            var movesContent = moves.Content.ReadAsAsync<List<MoveDto>>().Result;

            foreach (var move in movesContent)
            {
                var requestModel = new CalculatorMoveModel
                {
                    AttackerDamagePercent = 100,
                    CrouchingModifier = CrouchingModifier.NotCrouching,
                    ElectricModifier = ElectricModifier.NormalAttack,
                    HitboxOption = HitboxOptions.First,
                    Modifiers = Modifiers.Standing,
                    MoveId = move.Id,
                    ShieldAdvantageModifier = ShieldAdvantageModifier.Regular,
                    StaleMoveNegationMultiplier = StaleMoveNegationMultipler.S1,
                    TargetWeight = 80,
                    VictimDamagePercent = 70
                };

                var response =
                    await
                        LoggedInBasicClient.PostAsJsonAsync(Baseuri + CalculatorRoute + "/moves/vsknockback",
                            requestModel);

                //Assert.That(response.IsSuccessStatusCode);

                if (!response.IsSuccessStatusCode) continue;
                var result = await response.Content.ReadAsAsync<CompletedCalculationResponseDto>();

                responses.Add(result);
            }

            using (var writer = File.CreateText("test.csv"))
            {
                foreach (var result in responses)
                {

                    writer.WriteLine(
                        $"{result.CharacterName}, {result.MoveName}, {result.CalculatedValueName}, {result.CalculatedValue}");
                }
            }

        }

        [Test]
        [Explicit("Writes to csv file")]
        public async Task WriteToCSVTest_Shieldstun_Normal()
        {
            var responses = new List<CompletedCalculationResponseDto>();

            var moves = await LoggedInBasicClient.GetAsync(Baseuri + MovesRoute);

            var movesContent = moves.Content.ReadAsAsync<List<MoveDto>>().Result;

            foreach (var move in movesContent)
            {
                var requestModel = new CalculatorMoveModel
                {
                    AttackerDamagePercent = 100,
                    CrouchingModifier = CrouchingModifier.NotCrouching,
                    ElectricModifier = ElectricModifier.NormalAttack,
                    HitboxOption = HitboxOptions.First,
                    Modifiers = Modifiers.Standing,
                    MoveId = move.Id,
                    ShieldAdvantageModifier = ShieldAdvantageModifier.Regular,
                    StaleMoveNegationMultiplier = StaleMoveNegationMultipler.S1,
                    TargetWeight = 80,
                    VictimDamagePercent = 70
                };

                var response =
                    await
                        LoggedInBasicClient.PostAsJsonAsync(Baseuri + CalculatorRoute + "/moves/shieldstunnormal",
                            requestModel);

                //Assert.That(response.IsSuccessStatusCode);

                if (!response.IsSuccessStatusCode) continue;
                var result = await response.Content.ReadAsAsync<CompletedCalculationResponseDto>();

                responses.Add(result);
            }

            using (var writer = File.CreateText("test.csv"))
            {
                foreach (var result in responses)
                {

                    writer.WriteLine(
                        $"{result.CharacterName}, {result.MoveName}, {result.CalculatedValueName}, {result.CalculatedValue}");
                }
            }

        }
    }
}
