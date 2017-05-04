using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.WebApi.Tests.Controllers
{
    //[TestFixture]
    //public class CharacterAttributeControllerTests : BaseControllerTests
    //{
        //[SetUp]
        //public override void SetUp()
        //{
        //    Fixture.Customizations.Add(
        //        new TypeRelay(
        //            typeof(IAttribute),
        //            typeof(CharacterAttribute)));

        //    Fixture.Customizations.Add(
        //       new TypeRelay(
        //           typeof(ICharacterAttributeRow),
        //           typeof(DefaultCharacterAttributeRow)));
        //}

        //[Test]
        //public void ConstructorRejectsNullCharacterAttributeService()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        // ReSharper disable once ObjectCreationAsStatement
        //        new CharacterAttributeController(null);
        //    });
        //}

        //[Test]
        //public void Error_ReturnsNotFoundResultWhenAttributeDoesNotExist()
        //{
        //    var characterAttributeRepositoryMock = new Mock<IRepository<ICharacterAttributeRow>>();
        //    characterAttributeRepositoryMock.Setup(c => c.Get(It.IsInRange("0", "1", Range.Inclusive))).Returns(()
        //        => Fixture.Create<ICharacterAttributeRow>());

        //    var controller =
        //        new CharacterAttributeController(
        //            new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object));

        //    var response = controller.Get("-1") as NotFoundResult;

        //    Assert.That(response, Is.Not.Null);
        //}

        //[Test]
        //public void GetSingleCharacterAttributeRow()
        //{
        //    var fakeCharacterAttributeRow = Fixture.Create<ICharacterAttributeRow>();
        //    var fakeAttributes = fakeCharacterAttributeRow.Values.ToList();
        //    var firstFakeAttribute = fakeAttributes[0];

        //    var characterAttributeServiceMock = new Mock<ICharacterAttributeRowService>();
        //    characterAttributeServiceMock.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns(() => fakeCharacterAttributeRow);

        //    var controller = new CharacterAttributeController(characterAttributeServiceMock.Object);

        //    var response = controller.Get(firstFakeAttribute.Id) as OkNegotiatedContentResult<ICharacterAttributeRow>;

        //    Assert.That(response, Is.Not.Null);

        //    // ReSharper disable once PossibleNullReferenceException
        //    var attribute = response.Content;

        //    Assert.That(attribute.Name, Is.Not.Empty);
        //    Assert.That(attribute.Name, Is.EqualTo(fakeCharacterAttributeRow.Name), $"Character name was not equal to {fakeCharacterAttributeRow.Name}");
        //    Assert.That(attribute.Values.Count, Is.GreaterThan(0));
        //}

        //[Test]
        //public void GetManyCharacterAttributeRows()
        //{
        //    var characterAttributeServiceMock = new Mock<ICharacterAttributeRowService>();
        //    characterAttributeServiceMock.Setup(c => c.GetAll(It.IsAny<string>()))
        //        .Returns(() => Fixture.CreateMany<ICharacterAttributeRow>());

        //    var controller = new CharacterAttributeController(characterAttributeServiceMock.Object);

        //    var response = controller.GetAll() as OkNegotiatedContentResult<IEnumerable<ICharacterAttributeRow>>;

        //    Assert.That(response, Is.Not.Null);

        //    // ReSharper disable once PossibleNullReferenceException
        //    var attributes = response.Content.ToList();

        //    CollectionAssert.AllItemsAreUnique(attributes);
        //    CollectionAssert.IsNotEmpty(attributes);

        //    attributes.ForEach(attribute =>
        //    {
        //        Assert.That(attribute.Name, Is.Not.Empty);
        //        Assert.That(attribute.Name, Is.Not.Empty);
        //    });
        //}
    //}
}
