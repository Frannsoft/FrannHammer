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
    //public class MoveControllerTests : BaseControllerTests
    //{
    //    [SetUp]
    //    public override void SetUp()
    //    {
    //        Fixture.Customizations.Add(
    //            new TypeRelay(
    //                typeof(IMove),
    //                typeof(Move)));
    //    }

    //    [Test]
    //    public void ConstructorRejectsNullCharacterAttributeService()
    //    {
    //        Assert.Throws<ArgumentNullException>(() =>
    //        {
    //            // ReSharper disable once ObjectCreationAsStatement
    //            new MoveController(null);
    //        });
    //    }

    //    [Test]
    //    public void Error_ReturnsNotFoundResultWhenAttributeDoesNotExist()
    //    {
    //        var moveRepositoryMock = new Mock<IRepository<IMove>>();
    //        moveRepositoryMock.Setup(c => c.Get(It.IsInRange("0", "1", Range.Inclusive))).Returns(() => Fixture.Create<IMove>());

    //        var controller = new MoveController(new DefaultMoveService(moveRepositoryMock.Object));

    //        var response = controller.Get("-1") as NotFoundResult;

    //        Assert.That(response, Is.Not.Null);
    //    }

    //    [Test]
    //    public void GetAMoveName()
    //    {
    //        var testMove = Fixture.Create<IMove>();

    //        var moveServiceMock = new Mock<IMoveService>();
    //        moveServiceMock.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<string>()))
    //            .Returns(() => testMove);

    //        var controller = new MoveController(moveServiceMock.Object);

    //        var response = controller.Get(testMove.Id) as OkNegotiatedContentResult<IMove>;

    //        Assert.That(response, Is.Not.Null);

    //        // ReSharper disable once PossibleNullReferenceException
    //        var move = response.Content;

    //        Assert.That(move.Name, Is.Not.Empty);
    //        Assert.That(move.Name, Is.EqualTo(testMove.Name), $"move name was not equal to {testMove.Name}");
    //    }

    //    [Test]
    //    public void GetManyMoves()
    //    {
    //        var moveServiceMock = new Mock<IMoveService>();
    //        moveServiceMock.Setup(c => c.GetAll(It.IsAny<string>()))
    //            .Returns(() => Fixture.CreateMany<IMove>());

    //        var controller = new MoveController(moveServiceMock.Object);

    //        var response = controller.GetAll() as OkNegotiatedContentResult<IEnumerable<IMove>>;

    //        Assert.That(response, Is.Not.Null);

    //        // ReSharper disable once PossibleNullReferenceException
    //        var moves = response.Content.ToList();

    //        CollectionAssert.AllItemsAreUnique(moves);
    //        CollectionAssert.IsNotEmpty(moves);

    //        moves.ForEach(attribute =>
    //        {
    //            Assert.That(attribute.Name, Is.Not.Empty);
    //            Assert.That(attribute.Name, Is.Not.Empty);
    //        });
    //    }
    //}
}
