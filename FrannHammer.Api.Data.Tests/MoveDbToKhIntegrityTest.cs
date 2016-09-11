using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.WebScraper;
using FrannHammer.WebScraper.Stats;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class MoveDbToKhIntegrityTest : BaseDataIntegrityTest
    {
        private List<Move> _moves;
        private List<Character> _characters;


        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            _moves = Context.Moves.ToList();
            _characters = Context.Characters.ToList();
        }

        [Test]
        public void MoveDbDataIsEqualToKhPageData()
        {
            foreach (var characterFromDb in _characters)
            {
                var characterFromKhPage = new WebCharacter(characterFromDb);

                var unfilteredMovePageData = characterFromKhPage.FrameData.OfType<MoveStat>();
                var filteredMovesFromKhPage = FilterUnwantedRowsFromKhPageData(unfilteredMovePageData);

                var dbMovesForCharacter = _moves.Where(m => m.OwnerId == characterFromDb.Id).ToList();

                Assert.That(dbMovesForCharacter.Count, Is.GreaterThan(0));
                Assert.That(filteredMovesFromKhPage.Count, Is.GreaterThan(0));
                Assert.That(dbMovesForCharacter.Count, Is.EqualTo(filteredMovesFromKhPage.Count), $"Mismatch on move count for character '{characterFromDb.DisplayName}'");

                foreach (var move in WhereNotThrowMove(dbMovesForCharacter))
                {
                    var moveFromKhPage = filteredMovesFromKhPage.FirstOrDefault(khmove => khmove.Name.Equals(move.Name) &&
                                                                                          khmove.Angle.Equals(move.Angle) &&
                                                                                          khmove.AutoCancel.Equals(move.AutoCancel) &&
                                                                                          khmove.BaseDamage.Equals(move.BaseDamage) &&
                                                                                          khmove.BaseKnockBackSetKnockback.Equals(move.BaseKnockBackSetKnockback) &&
                                                                                          khmove.FirstActionableFrame.Equals(move.FirstActionableFrame) &&
                                                                                          khmove.HitboxActive.Equals(move.HitboxActive) &&
                                                                                          khmove.KnockbackGrowth.Equals(move.KnockbackGrowth) &&
                                                                                          khmove.LandingLag.Equals(move.LandingLag) &&
                                                                                          khmove.OwnerId == move.OwnerId);
                    Assert.That(moveFromKhPage, Is.Not.Null);
                }
            }
        }

        private IList<Move> WhereNotThrowMove(IList<Move> moves) => moves.Where(m => !m.Name.Contains("Fthrow") &&
                                                                                     !m.Name.Contains("Bthrow") &&
                                                                                     !m.Name.Contains("Uthrow") &&
                                                                                     !m.Name.Contains("Dthrow")).ToList();

        private IList<MoveStat> FilterUnwantedRowsFromKhPageData(IEnumerable<MoveStat> unfilteredMoveStats)
        {
            return unfilteredMoveStats.Where(stat => !stat.Name.Equals("Grabs") &&
                                                     !stat.Name.Equals("Throws") &&
                                                     !stat.Name.Equals("Miscellaneous") &&
                                                     !stat.Name.Equals("Spotdodge") &&
                                                     !stat.Name.Equals("Forward Roll") &&
                                                     !stat.Name.Equals("Back Roll") &&
                                                     !stat.Name.Equals("Useless Tractor Beams") && //pac-man
                                                     !stat.Name.Equals("Airdodge")).ToList();
        }
    }
}
