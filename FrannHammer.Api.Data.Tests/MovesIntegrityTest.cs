using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services;
using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class MovesIntegrityTest : BaseDataIntegrityTest
    {
        [Test]
        public void MovesCountIsExpectedAmount()
        {
            var moves = Context.Moves;

            Assert.That(moves.ToList().Count, Is.EqualTo(2845));
        }

        [Test]
        public void EveryMoveHasName()
        {
            var moves = Context.Moves.ToList();

            moves.ForEach(m => Assert.That(!string.IsNullOrEmpty(m.Name)));
        }

        [Test]
        public void EveryMoveHasOwnerId()
        {
            var moves = Context.Moves.ToList();

            moves.ForEach(m => Assert.That(m.OwnerId > 0));
        }

        [Test]
        [TestCase(1)]
        public void NonSearchMoveDoesNotHaveOwnerProperty(int moveId)
        {
            var metadataService = new MetadataService(Context, new ResultValidationService());
            var controller = new MovesController(metadataService, null);

            var response = controller.GetMove(moveId) as OkNegotiatedContentResult<ExpandoObject>;
            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            IDictionary<string, object> content = response.Content;

            Assert.That(content, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(!content.Keys.ToList().Exists(k => k.Equals("Owner")), "Owner property found!");
        }

        [Test]
        [TestCase(1)]
        public void SearchMoveHasOwnerPropertyInResult(int moveId)
        {
            var metadataService = new MetadataService(Context, new ResultValidationService());
            var controller = new MovesController(metadataService, null);

            var searchModel = new MoveSearchModel
            {
                Name = "jab 1"
            };

            // ReSharper disable once PossibleNullReferenceException
            IEnumerable<dynamic> response = (controller.MovesThatMeetCriteria(searchModel) as OkNegotiatedContentResult<IEnumerable<dynamic>>).Content;

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(response.Count(), Is.GreaterThan(0));

            // ReSharper disable once PossibleNullReferenceException
            IDictionary<string, object> firstContentObject = response.First();

            Assert.That(firstContentObject.Keys.ToList().Exists(k => k.Equals("Owner")), "Owner property NOT found!");
            Assert.That(firstContentObject["Owner"].GetType(), Is.EqualTo(typeof(CharacterDto)), "Incorrect type detected for Owner property");
        }
    }
}
