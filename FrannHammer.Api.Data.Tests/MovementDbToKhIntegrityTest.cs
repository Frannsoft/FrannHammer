using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.WebScraper;
using FrannHammer.WebScraper.Stats;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    [LongRunning]
    public class MovementDbToKhIntegrityTest : BaseDataIntegrityTest
    {
        private List<Movement> _movements;
        private List<Character> _characters;


        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            _movements = Context.Movements.ToList();
            _characters = Context.Characters.ToList();
        }

        [Test]
        public void MovementDbDataIsEqualToKhPageData()
        {
            foreach (var characterFromDb in _characters)
            {
                var characterFromKhPage = new WebCharacter(characterFromDb);

                var movementsFromKhPage = characterFromKhPage.FrameData.OfType<MovementStat>().ToList();

                var dbMovementsForCharacter = _movements.Where(m => m.OwnerId == characterFromDb.Id).ToList();

                Assert.That(dbMovementsForCharacter.Count, Is.GreaterThan(0));
                Assert.That(movementsFromKhPage.Count, Is.GreaterThan(0));
                Assert.That(dbMovementsForCharacter.Count, Is.EqualTo(movementsFromKhPage.Count));

                foreach (var movement in dbMovementsForCharacter)
                {
                    var movementFromKhPage = movementsFromKhPage.FirstOrDefault(khmove => khmove.Name.Equals(movement.Name) &&
                                                                                          khmove.Value.Equals(movement.Value) &&
                                                                                          khmove.OwnerId == movement.OwnerId);
                    Assert.That(movementFromKhPage, Is.Not.Null);
                    Assert.That(movementFromKhPage, Is.Not.Null, $"Unable to find matching kh move for {movement.Name} - {characterFromDb.DisplayName} (id = {characterFromDb.Id})");

                }
            }
        }
    }
}
