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
using Ploeh.AutoFixture.AutoMoq;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture(typeof(ICharacter), typeof(Character), typeof(DefaultCharacterService), typeof(CharacterController))]
    [TestFixture(typeof(ICharacterAttributeRow), typeof(DefaultCharacterAttributeRow), typeof(DefaultCharacterAttributeService), typeof(CharacterAttributeController))]
    [TestFixture(typeof(IMove), typeof(Move), typeof(DefaultMoveService), typeof(MoveController))]
    [TestFixture(typeof(IMovement), typeof(Movement), typeof(DefaultMovementService), typeof(MovementController))]
    public class GeneralControllerTests<TModelInterface, TModel, TService, TSut>
        where TModelInterface : IModel
        where TModel : TModelInterface
        where TService : ICrudService<TModelInterface>
        where TSut : BaseApiController
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void GetByIdReturnsExpectedItem()
        {
            var anonymousTestItem = _fixture.Create<TModel>();
            var expectedTestItem = _fixture.Create<TModel>();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.Get(It.IsInRange("0", anonymousTestItem.Id, Range.Inclusive))).Returns(() => anonymousTestItem);
            repositoryMock.Setup(c => c.Get(It.Is<string>(id => id == expectedTestItem.Id))).Returns(() => expectedTestItem);

            var crudService = CreateCrudService(repositoryMock.Object);
            var sut = CreateController(crudService);

            var response = sut.Get(expectedTestItem.Id) as OkNegotiatedContentResult<TModelInterface>;

            Assert.That(response, Is.Not.Null, $"{nameof(response)} was null.");

            // ReSharper disable once PossibleNullReferenceException
            var result = response.Content;

            Assert.That(result.Id, Is.Not.Null, $"{nameof(IModel.Id)} is null.");
            Assert.That(result.Id, Is.EqualTo(expectedTestItem.Id));

            Assert.That(result.Name, Is.Not.Null, $"{nameof(IModel.Name)} is null.");
            Assert.That(result.Name, Is.EqualTo(expectedTestItem.Name));
        }

        [Test]
        public void GetAllReturnsAllStoredItems()
        {
            var items = _fixture.CreateMany<TModel>().Cast<TModelInterface>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetAll()).Returns(() => items);

            var crudService = CreateCrudService(repositoryMock.Object);
            var sut = CreateController(crudService);

            var response = sut.GetAll() as OkNegotiatedContentResult<IEnumerable<TModelInterface>>;

            Assert.That(response, Is.Not.Null, $"{nameof(response)} was null.");

            // ReSharper disable once PossibleNullReferenceException
            var result = response.Content.ToList();

            Assert.That(result.Count, Is.EqualTo(items.Count));
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreUnique(result);

            result.ForEach(item =>
            {
                Assert.That(item.Id, Is.Not.Null, $"{nameof(IModel.Id)} is null.");
                Assert.That(item.Name, Is.Not.Null, $"{nameof(IModel.Name)} is null.");
            });
        }

        [Test]
        public void Error_ReturnsNotFoundResultWhenAttributeDoesNotExist()
        {
            const string id = "-1";

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.Get(It.IsInRange("0", "1", Range.Inclusive))).Returns(()
                => _fixture.Create<TModelInterface>());

            var crudService = CreateCrudService(repositoryMock.Object);
            var sut = CreateController(crudService);

            var response = sut.Get(id) as NotFoundResult;

            Assert.That(response, Is.Not.Null, $"Did not expect to find a result for id of '{id}'");
        }

        private static TSut CreateController(TService crudService)
        {
            return (TSut)Activator.CreateInstance(typeof(TSut), crudService);
        }

        private static TService CreateCrudService(IRepository<TModelInterface> repository)
        {
            return (TService)Activator.CreateInstance(typeof(TService), repository);
        }
    }
}
