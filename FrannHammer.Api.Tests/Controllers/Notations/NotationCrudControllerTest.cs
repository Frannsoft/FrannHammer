using System;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.Notations
{
    [TestFixture]
    public class NotationCrudControllerTest : BaseControllerTest
    {
        [Test]
        public void ShouldGetNotations()
        {
            var notation = TestObjects.Notation();
            NotationsController.PostNotation(notation);

            var result = NotationsController.GetNotation(notation.Id) as OkNegotiatedContentResult<Notation>;

            Assert.That(result?.Content, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllNotations()
        {
            var notation = TestObjects.Notation();
            NotationsController.PostNotation(notation);

            var results = NotationsController.GetNotations();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddNotation()
        {
            var notation = TestObjects.Notation();
            var result = NotationsController.PostNotation(notation) as CreatedAtRouteNegotiatedContentResult<Notation>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(notation, result?.Content);
        }

        [Test]
        public void ShouldUpdateNotation()
        {
            const string expectedName = "mewtwo";
            var notation = TestObjects.Notation();

            var dateTime = DateTime.Now;

            //arrange
            var returnedNotation =
                NotationsController.PostNotation(notation) as CreatedAtRouteNegotiatedContentResult<Notation>;
            //act
            if (returnedNotation != null)
            {
                returnedNotation.Content.Name = expectedName;
                NotationsController.PutNotation(returnedNotation.Content.Id, returnedNotation.Content);
            }

            var updatedCharacter = NotationsController.GetNotation(notation.Id) as OkNegotiatedContentResult<Notation>;

            //assert
            Assert.That(updatedCharacter?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteNotation()
        {
            var notation = TestObjects.Notation();
            NotationsController.PostNotation(notation);

            NotationsController.DeleteNotation(notation.Id);

            var result = NotationsController.GetNotation(notation.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}
