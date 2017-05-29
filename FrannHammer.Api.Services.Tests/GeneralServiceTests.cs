using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Api.Services.Tests.ApiServiceFactories;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture(typeof(IMove), typeof(Move), typeof(DefaultMoveService), typeof(MoveApiServiceFactory))]
    [TestFixture(typeof(IMovement), typeof(Movement), typeof(DefaultMovementService), typeof(MovementApiServiceFactory))]
    [TestFixture(typeof(ICharacter), typeof(Character), typeof(DefaultCharacterService), typeof(DefaultCharacterServiceFactory))]
    [TestFixture(typeof(ICharacterAttributeRow), typeof(CharacterAttributeRow), typeof(DefaultCharacterAttributeService), typeof(DefaultCharacterAttributeServiceFactory))]
    public class GeneralServiceTests<TModelInterface, TModel, TSut, TSutFactory>
        where TModelInterface : class, IModel
        where TModel : TModelInterface
        where TSut : ICrudService<TModelInterface>
        where TSutFactory : ApiServiceFactory<TModelInterface>
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void CanAddSingleItemToRepository()
        {
            var repositoryItems = _fixture.CreateMany<TModelInterface>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetAll()).Returns(() => repositoryItems);
            repositoryMock.Setup(c => c.Add(It.IsAny<TModelInterface>())).Callback<TModelInterface>(c =>
            {
                repositoryItems.Add(c);
            });
            var sut = CreateCrudService(repositoryMock.Object);

            int previousCount = sut.GetAll().Count();

            var newItem = _fixture.Create<TModelInterface>();

            sut.Add(newItem);

            int newCount = sut.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void CanAddManyItemsToRepository()
        {
            var repositoryItems = _fixture.CreateMany<TModelInterface>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetAll()).Returns(() => repositoryItems);
            repositoryMock.Setup(c => c.AddMany(It.IsAny<IEnumerable<TModelInterface>>())).Callback<IEnumerable<TModelInterface>>(c =>
            {
                repositoryItems.AddRange(c);
            });

            var sut = CreateCrudService(repositoryMock.Object);

            int previousCount = sut.GetAll().Count();

            var newItems = _fixture.CreateMany<TModelInterface>().ToList();

            sut.AddMany(newItems);

            int newCount = sut.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + newItems.Count));
        }

        [Test]
        public void GetSingleItemWhereCallsRepositoryGetSingleWhereNameIsMethod()
        {
            var items = _fixture.CreateMany<TModel>().ToList();
            string name = items.First().Name;

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(r => r.GetSingleWhere(It.IsAny<Func<TModelInterface, bool>>())).Returns((Func<TModel, bool> where) => items.Single(where));

            var sut = CreateCrudService(repositoryMock.Object);

            sut.GetSingleWhere(t => t.Name == name);

            repositoryMock.VerifyAll();
        }

        [Test]
        public void GetItemByNameReturnsItemWithExpectedName()
        {
            var items = _fixture.CreateMany<TModel>().ToList();
            string name = items.First().Name;

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(r => r.GetSingleWhere(It.IsAny<Func<TModelInterface, bool>>())).Returns((Func<TModel, bool> where) => items.Single(where));

            var sut = CreateCrudService(repositoryMock.Object);

            var result = sut.GetSingleWhere(t => t.Name == name);

            Assert.That(result, Is.Not.Null, $"{nameof(result)}");
            Assert.That(result.Name, Is.EqualTo(name), $"{nameof(result.Name)}");
        }

        [Test]
        public void ReturnsNullForNoItemFoundById()
        {
            var items = _fixture.CreateMany<TModel>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetSingleWhere(It.IsAny<Func<TModelInterface, bool>>())).Returns((Func<TModel, bool> where) => items.SingleOrDefault(where));

            var sut = CreateCrudService(repositoryMock.Object);

            var result = sut.GetSingleById("0");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ReturnsNullForNoItemsFoundByName()
        {
            var items = _fixture.CreateMany<TModel>().ToList();

            var repositoryMock = new Mock<IRepository<TModelInterface>>();
            repositoryMock.Setup(c => c.GetAllWhere(It.IsAny<Func<TModelInterface, bool>>())).Returns((Func<TModel, bool> where) => items.Where(where).Cast<TModelInterface>());

            var sut = CreateCrudService(repositoryMock.Object);

            var results = sut.GetAllWhereName("testName");

            Assert.That(results, Is.Empty);
        }

        [Test]
        public void RejectsNullItemForAddition()
        {
            var repositoryMock = new Mock<IRepository<TModelInterface>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var sut = CreateCrudService(repositoryMock.Object);
                sut.Add(null);
            });
        }

        /// <summary>
        /// Using the <see cref="ApiServiceFactory{T}"/> classes to generate these allows me to use this generic test fixture
        /// for cumbersome (but important) tests while offloading all the dependencies to the factories.
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        private static ICrudService<TModelInterface> CreateCrudService(IRepository<TModelInterface> repository)
        {
            var serviceFactory = (TSutFactory) Activator.CreateInstance(typeof(TSutFactory));

            return serviceFactory.CreateService(repository);
        }
    }
}
