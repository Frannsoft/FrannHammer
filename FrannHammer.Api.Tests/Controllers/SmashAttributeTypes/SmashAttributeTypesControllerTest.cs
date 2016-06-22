using System;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.SmashAttributeTypes
{
    [TestFixture]
    public class SmashAttributeTypesControllerTest : BaseControllerTest
    {

        [Test]
        public void ShouldGetSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            SmashAttributeTypesController.PostSmashAttributeType(smashAttributeType);

            var result = SmashAttributeTypesController.GetSmashAttributeType(smashAttributeType.Id) as OkNegotiatedContentResult<SmashAttributeType>;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            SmashAttributeTypesController.PostSmashAttributeType(smashAttributeType);

            var results = SmashAttributeTypesController.GetSmashAttributeTypes();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            var result = SmashAttributeTypesController.PostSmashAttributeType(smashAttributeType) as CreatedAtRouteNegotiatedContentResult<SmashAttributeType>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(smashAttributeType, result?.Content);
        }

        [Test]
        public void ShouldUpdateSmashAttributeTypes()
        {
            const string expectedName = "new name";
            var smashAttributeType = TestObjects.SmashAttributeType();

            var dateTime = DateTime.Now;

            var returnedSmashAttributeTypes =
                SmashAttributeTypesController.PostSmashAttributeType(smashAttributeType) as CreatedAtRouteNegotiatedContentResult<SmashAttributeType>;

            if (returnedSmashAttributeTypes != null)
            {
                returnedSmashAttributeTypes.Content.Name = expectedName;
                SmashAttributeTypesController.PutSmashAttributeType(returnedSmashAttributeTypes.Content.Id, returnedSmashAttributeTypes.Content);
            }

            var updatedSmashAttributeTypes = SmashAttributeTypesController.GetSmashAttributeType(smashAttributeType.Id) as OkNegotiatedContentResult<SmashAttributeType>;

            Assert.That(updatedSmashAttributeTypes?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedSmashAttributeTypes?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteSmashAttributeTypes()
        {
            var smashAttributeType = TestObjects.SmashAttributeType();
            SmashAttributeTypesController.PostSmashAttributeType(smashAttributeType);

            SmashAttributeTypesController.DeleteSmashAttributeType(smashAttributeType.Id);

            var result = SmashAttributeTypesController.GetSmashAttributeType(smashAttributeType.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}
